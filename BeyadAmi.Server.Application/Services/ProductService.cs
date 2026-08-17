using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Products;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly CreateProductValidator _validator = new();

        public ProductService(IProductRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(productId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<ProductDto>> SearchAsync(string? searchText, CancellationToken cancellationToken = default)
        {
            var results = await _repository.SearchAsync(searchText, cancellationToken);
            return results.Select(MapToDto).ToList();
        }

        public async Task<int> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            if (await _repository.ExistsAsync(dto.ProductName!, dto.Model!, dto.Company, cancellationToken))
                throw new ProductAlreadyExistsException(dto.ProductName!, dto.Model!, dto.Company);

            var entity = new Product
            {
                ProductName = dto.ProductName,
                Model = dto.Model,
                Company = dto.Company,
                Notes = dto.Notes
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.ProductId;
        }

        public async Task UpdateAsync(int productId, UpdateProductDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(productId, cancellationToken)
                ?? throw new ProductNotFoundException(productId);

            if (string.IsNullOrWhiteSpace(dto.ProductName))
                throw new BusinessException("שם המוצר הוא שדה חובה.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                throw new BusinessException("הדגם הוא שדה חובה.");

            var nameChanged = !string.Equals(existing.ProductName, dto.ProductName, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(existing.Model, dto.Model, StringComparison.OrdinalIgnoreCase)
                || !string.Equals(existing.Company, dto.Company, StringComparison.OrdinalIgnoreCase);

            if (nameChanged && await _repository.ExistsAsync(dto.ProductName!, dto.Model!, dto.Company, cancellationToken))
                throw new ProductAlreadyExistsException(dto.ProductName!, dto.Model!, dto.Company);

            existing.ProductName = dto.ProductName;
            existing.Model = dto.Model;
            existing.Company = dto.Company;
            existing.Notes = dto.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int productId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(productId, cancellationToken)
                ?? throw new ProductNotFoundException(productId);

            if (await _repository.HasPurchasesAsync(productId, cancellationToken))
                throw new ProductHasPurchasesException(productId);

            _repository.Delete(existing);
        }

        private static ProductDto MapToDto(Product p) => new()
        {
            ProductId = p.ProductId,
            ProductName = p.ProductName,
            Model = p.Model,
            Company = p.Company,
            Notes = p.Notes
        };
    }
}
