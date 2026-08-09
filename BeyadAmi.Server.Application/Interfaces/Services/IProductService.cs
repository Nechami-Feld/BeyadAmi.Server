using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Products;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IProductService
    {
        Task<ProductDto?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
        Task<List<ProductDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<ProductDto>> SearchAsync(string? searchText, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateProductDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int productId, UpdateProductDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int productId, CancellationToken cancellationToken = default);
    }
}
