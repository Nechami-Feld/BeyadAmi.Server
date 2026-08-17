using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Loans;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class LoanService : ILoanService
    {
        private const int DepositTypeNoneId = 3;

        private readonly ILoanRepository _repository;
        private readonly IDeviceRepository _deviceRepository;
        private readonly CreateLoanValidator _validator = new();

        public LoanService(ILoanRepository repository, IDeviceRepository deviceRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
        }

        public async Task<int> CreateAsync(CreateLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var device = await _deviceRepository.GetByIdAsync(dto.DeviceId, cancellationToken)
                ?? throw new DeviceNotFoundException(dto.DeviceId);

            if (await _repository.HasActiveLoanAsync(dto.DeviceId, cancellationToken))
                throw new DeviceAlreadyLoanedException(dto.DeviceId);

            var entity = new Loan
            {
                DeviceId = dto.DeviceId,
                LastName = dto.BorrowerLastName,
                Address = dto.Address,
                Phone = dto.Phone,
                DepositTypeId = dto.DepositTypeId,
                LoanDate = DateTime.UtcNow,
                Notes = dto.Notes
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.LoanId;
        }

        public async Task ReturnAsync(int loanId, ReturnLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(loanId, cancellationToken)
                ?? throw new LoanNotFoundException(loanId);

            existing.ReturnDate = dto.ReturnDate ?? DateTime.UtcNow;
            existing.Notes = dto.Notes ?? existing.Notes;

            _repository.Update(existing);
        }

        public async Task UpdateAsync(int loanId, UpdateLoanDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(loanId, cancellationToken)
                ?? throw new LoanNotFoundException(loanId);

            if (string.IsNullOrWhiteSpace(dto.BorrowerLastName))
                throw new BusinessException("שם משפחת השואל הוא שדה חובה.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                throw new BusinessException("מספר הטלפון הוא שדה חובה.");

            if (dto.DepositTypeId <= 0)
                throw new BusinessException("נדרש מזהה סוג פיקדון.");

            existing.LastName = dto.BorrowerLastName;
            existing.Address = dto.Address;
            existing.Phone = dto.Phone;
            existing.DepositTypeId = dto.DepositTypeId;
            existing.Notes = dto.Notes ?? existing.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int loanId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(loanId, cancellationToken);
            if (existing == null) return;

            if (existing.IsActive)
                throw new ActiveLoanCannotBeDeletedException(loanId);

            _repository.Delete(existing);
        }

        public async Task<LoanDto?> GetByIdAsync(int loanId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(loanId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<LoanDto>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetActiveAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<LoanDto>> GetByDeviceAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetByDeviceAsync(deviceId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static LoanDto MapToDto(Loan l) => new()
        {
            LoanId = l.LoanId,
            DeviceId = l.DeviceId,
            DeviceNumber = l.Device?.DeviceNumber,
            BranchName = l.Device?.Branch?.BranchName,
            BorrowerLastName = l.LastName,
            Address = l.Address,
            Phone = l.Phone,
            DepositTypeId = l.DepositTypeId,
            DepositTypeName = l.DepositType?.DepositTypeName,
            LoanDate = l.LoanDate,
            ReturnDate = l.ReturnDate,
            IsActive = l.IsActive,
            Notes = l.Notes
        };
    }
}
