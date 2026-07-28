using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Purchases;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _repository;
        private readonly CreatePurchaseValidator _validator = new();

        public PurchaseService(IPurchaseRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreatePurchaseDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var entity = new Purchase
            {
                StoreId = dto.StoreId,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                PricePerUnit = dto.PricePerUnit,
                TotalPrice = dto.Quantity * dto.PricePerUnit,
                BuyerName = dto.PurchasedBy,
                PurchaseDate = dto.PurchaseDate ?? DateTime.UtcNow,
                ReceiptFile = dto.Receipt,
                Notes = dto.Notes
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.PurchaseId;
        }

        public async Task UpdateAsync(int purchaseId, UpdatePurchaseDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(purchaseId, cancellationToken)
                ?? throw new PurchaseNotFoundException(purchaseId);

            if (dto.Quantity <= 0)
                throw new InvalidPurchaseException("Quantity must be greater than 0.");

            if (dto.PricePerUnit < 0)
                throw new InvalidPurchaseException("PricePerUnit must be greater than or equal to 0.");

            existing.StoreId = dto.StoreId;
            existing.ProductId = dto.ProductId;
            existing.Quantity = dto.Quantity;
            existing.PricePerUnit = dto.PricePerUnit;
            existing.TotalPrice = dto.Quantity * dto.PricePerUnit;
            existing.BuyerName = dto.PurchasedBy;
            existing.ReceiptFile = dto.Receipt;
            existing.Notes = dto.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int purchaseId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(purchaseId, cancellationToken);
            if (existing == null) return;

            _repository.Delete(existing);
        }

        public async Task<PurchaseDto?> GetByIdAsync(int purchaseId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(purchaseId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<PurchaseDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<PurchaseDto>> GetByStoreAsync(int storeId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetByStoreAsync(storeId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<PurchaseDto>> GetByProductAsync(int productId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetByProductAsync(productId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static PurchaseDto MapToDto(Purchase p) => new()
        {
            PurchaseId = p.PurchaseId,
            StoreId = p.StoreId,
            StoreName = p.Store?.StoreName,
            ProductId = p.ProductId,
            ProductName = p.Product?.ProductName,
            ProductModel = p.Product?.Model,
            ProductCompany = p.Product?.Company,
            Quantity = p.Quantity,
            PricePerUnit = p.PricePerUnit,
            TotalPrice = p.Quantity * p.PricePerUnit,
            PurchasedBy = p.BuyerName,
            PurchaseDate = p.PurchaseDate,
            Receipt = p.ReceiptFile,
            Notes = p.Notes
        };
    }
}
