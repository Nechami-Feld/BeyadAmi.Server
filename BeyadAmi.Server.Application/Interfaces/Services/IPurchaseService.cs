using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Purchases;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IPurchaseService
    {
        Task<PurchaseDto?> GetByIdAsync(int purchaseId, CancellationToken cancellationToken = default);
        Task<List<PurchaseDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<PurchaseDto>> GetByStoreAsync(int storeId, CancellationToken cancellationToken = default);
        Task<List<PurchaseDto>> GetByProductAsync(int productId, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreatePurchaseDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int purchaseId, UpdatePurchaseDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int purchaseId, CancellationToken cancellationToken = default);
    }
}
