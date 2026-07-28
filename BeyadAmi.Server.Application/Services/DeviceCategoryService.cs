using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class DeviceCategoryService : IDeviceCategoryService
    {
        private readonly IDeviceCategoryRepository _repository;
        private readonly CreateDeviceCategoryValidator _validator = new();

        public DeviceCategoryService(IDeviceCategoryRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateDeviceCategoryDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            if (await _repository.ExistsByNameAsync(dto.CategoryName!, cancellationToken))
                throw new BusinessException($"CategoryName '{dto.CategoryName}' already exists.");

            var entity = new DeviceCategory
            {
                CategoryName = dto.CategoryName,
                Description = dto.Description
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.CategoryId;
        }

        public async Task UpdateAsync(int categoryId, UpdateDeviceCategoryDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(categoryId, cancellationToken)
                ?? throw new DeviceCategoryNotFoundException(categoryId);

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                throw new BusinessException("CategoryName is required.");

            if (!string.Equals(existing.CategoryName, dto.CategoryName, StringComparison.OrdinalIgnoreCase)
                && await _repository.ExistsByNameAsync(dto.CategoryName, cancellationToken))
                throw new BusinessException($"CategoryName '{dto.CategoryName}' already exists.");

            existing.CategoryName = dto.CategoryName;
            existing.Description = dto.Description;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(categoryId, cancellationToken);
            if (existing == null) return;

            if (await _repository.HasDeviceTypesAsync(categoryId, cancellationToken))
                throw new DeviceCategoryInUseException(categoryId);

            _repository.Delete(existing);
        }

        public async Task<DeviceCategoryDto?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(categoryId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<DeviceCategoryDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static DeviceCategoryDto MapToDto(DeviceCategory c) => new()
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName,
            Description = c.Description,
            DeviceTypesCount = c.DeviceTypes?.Count ?? 0
        };
    }
}
