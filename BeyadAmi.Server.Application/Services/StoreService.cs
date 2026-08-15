using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Stores;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _repository;
        private readonly CreateStoreValidator _validator = new();

        public StoreService(IStoreRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateStoreDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var entity = new Store
            {
                StoreName = dto.StoreName ?? string.Empty,
                IsActive = dto.IsActive,
                Address = dto.Address,
                Phone = dto.Phone,
                Email = dto.Email,
                Notes = dto.Notes
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.StoreId;
        }

        public async Task UpdateAsync(int storeId, UpdateStoreDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(storeId, cancellationToken);
            if (existing == null) throw new StoreNotFoundException(storeId);

            if (string.IsNullOrWhiteSpace(dto.StoreName))
                throw new BusinessException("StoreName is required.");

            existing.IsActive = dto.IsActive;
            existing.StoreName = dto.StoreName;
            existing.Address = dto.Address;
            existing.Phone = dto.Phone;
            existing.Email = dto.Email;
            existing.Notes = dto.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int storeId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(storeId, cancellationToken);
            if (existing == null) return; // idempotent

            var hasProducts = await _repository.HasProductsAsync(storeId, cancellationToken);
            if (hasProducts)
                throw new StoreHasProductsException("Cannot delete store that has products.");

            _repository.Delete(existing);
        }

        public async Task<StoreDto?> GetByIdAsync(int storeId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(storeId, cancellationToken);
            if (entity == null) throw new StoreNotFoundException(storeId);

            return MapToDto(entity);
        }

        public async Task<List<StoreDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static StoreDto MapToDto(Store s) => new StoreDto
        {
            StoreId = s.StoreId,
            StoreName = s.StoreName,
            IsActive = s.IsActive,
            Address = s.Address,
            Phone = s.Phone,
            Email = s.Email,
            Notes = s.Notes,
            ProductsCount = s.StoreProducts?.Count ?? 0
        };
    }
}
