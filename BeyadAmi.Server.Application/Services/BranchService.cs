using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Branches;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class BranchService : IBranchService
    {
        private readonly IBranchRepository _repository;
        private readonly CreateBranchValidator _validator = new();

        public BranchService(IBranchRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateBranchDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var entity = new Branch
            {
                BranchName = dto.BranchName ?? string.Empty,
                City = dto.City,
                Street = dto.Street,
                Apartment = dto.Apartment,
                Phone = dto.Phone,
                Email = dto.Email,
                Notes = dto.Notes,
                IsActive = true
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.BranchId;
        }

        public async Task UpdateAsync(int branchId, UpdateBranchDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(branchId, cancellationToken);
            if (existing == null) throw new BranchNotFoundException(branchId);

            // BranchName business rule: required
            if (string.IsNullOrWhiteSpace(dto.BranchName))
                throw new BusinessException("שם הסניף הוא שדה חובה.");

            existing.BranchName = dto.BranchName;
            existing.City = dto.City;
            existing.Street = dto.Street;
            existing.Apartment = dto.Apartment;
            existing.Phone = dto.Phone;
            existing.Email = dto.Email;
            existing.Notes = dto.Notes;
            existing.IsActive = dto.IsActive;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int branchId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(branchId, cancellationToken);
            if (existing == null) return; // idempotent

            // Rule: cannot delete branch that has devices
            if (existing.Devices != null && existing.Devices.Any())
                throw new BranchHasDevicesException("לא ניתן למחוק סניף שיש לו מכשירים.");

            // Rule: cannot delete branch that has active loans
            var hasActiveLoan = existing.Devices != null && existing.Devices.Any(d => d.Loans != null && d.Loans.Any(l => l.ReturnDate == null));
            if (hasActiveLoan)
                throw new BranchHasDevicesException("לא ניתן למחוק סניף שיש לו השאלות פעילות.");

            _repository.Delete(existing);
        }

        public async Task<BranchDto?> GetByIdAsync(int branchId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(branchId, cancellationToken);
            if (entity == null) return null;

            return MapToDto(entity);
        }

        public async Task<List<BranchDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static BranchDto MapToDto(Branch b) => new BranchDto
        {
            BranchId = b.BranchId,
            BranchName = b.BranchName,
            City = b.City,
            Street = b.Street,
            Apartment = b.Apartment,
            Phone = b.Phone,
            Email = b.Email,
            Notes = b.Notes,
            IsActive = b.IsActive,
            DevicesCount = b.Devices?.Count ?? 0
        };
    }
}
