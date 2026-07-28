using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Stores;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IStoreService
    {
        Task<StoreDto?> GetByIdAsync(int storeId, CancellationToken cancellationToken = default);
        Task<List<StoreDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateStoreDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int storeId, UpdateStoreDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int storeId, CancellationToken cancellationToken = default);
    }
}
