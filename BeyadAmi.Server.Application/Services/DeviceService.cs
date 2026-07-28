using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Device;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _repository;
        private readonly CreateDeviceValidator _validator = new();

        public DeviceService(IDeviceRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateDeviceDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            if (await _repository.ExistsByNumberAsync(dto.DeviceNumber!, cancellationToken))
                throw new DeviceAlreadyExistsException(dto.DeviceNumber!);

            var entity = new Device
            {
                CategoryId = dto.CategoryId,
                BranchId = dto.BranchId,
                DeviceNumber = dto.DeviceNumber,
                Company = dto.Company,
                Notes = dto.Notes,
                CreatedDate = DateTime.UtcNow
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.DeviceId;
        }

        public async Task UpdateAsync(int deviceId, UpdateDeviceDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(deviceId, cancellationToken)
                ?? throw new DeviceNotFoundException(deviceId);

            existing.CategoryId = dto.CategoryId;
            existing.BranchId = dto.BranchId;
            existing.Company = dto.Company;
            existing.Notes = dto.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(deviceId, cancellationToken);
            if (existing == null) return;

            if (await _repository.HasActiveLoansAsync(deviceId, cancellationToken))
                throw new DeviceHasActiveLoanException(deviceId);

            _repository.Delete(existing);
        }

        public async Task<DeviceDto?> GetByIdAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(deviceId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<DeviceDto>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetByBranchAsync(branchId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<DeviceDto>> GetAvailableAsync(int branchId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAvailableAsync(branchId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static DeviceDto MapToDto(Device d) => new()
        {
            DeviceId = d.DeviceId,
            DeviceNumber = d.DeviceNumber,
            CategoryId = d.CategoryId,
            CategoryName = d.Category?.CategoryName,
            BranchId = d.BranchId,
            BranchName = d.Branch?.BranchName,
            Company = d.Company,
            IsAvailable = !d.IsLoaned,
            Notes = d.Notes
        };
    }
}
