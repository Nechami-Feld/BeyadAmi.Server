using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetByIdAsync(int purchaseId, CancellationToken cancellationToken = default);
        Task<List<Purchase>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Purchase>> GetByStoreAsync(int storeId, CancellationToken cancellationToken = default);
        Task<List<Purchase>> GetByProductAsync(int productId, CancellationToken cancellationToken = default);
        Task AddAsync(Purchase purchase, CancellationToken cancellationToken = default);
        void Update(Purchase purchase);
        void Delete(Purchase purchase);
    }
}
