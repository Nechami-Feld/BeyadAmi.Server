using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int productId, CancellationToken cancellationToken = default);
        Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Product>> SearchAsync(string? searchText, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string productName, string model, string? company, CancellationToken cancellationToken = default);
        Task<bool> HasPurchasesAsync(int productId, CancellationToken cancellationToken = default);
        Task AddAsync(Product product, CancellationToken cancellationToken = default);
        void Update(Product product);
        void Delete(Product product);
    }
}
