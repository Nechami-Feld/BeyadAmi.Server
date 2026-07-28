using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(int storeId, CancellationToken cancellationToken = default);
        Task<List<Store>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int storeId, CancellationToken cancellationToken = default);
        Task<bool> HasProductsAsync(int storeId, CancellationToken cancellationToken = default);
        Task AddAsync(Store store, CancellationToken cancellationToken = default);
        void Update(Store store);
        void Delete(Store store);
    }
}
