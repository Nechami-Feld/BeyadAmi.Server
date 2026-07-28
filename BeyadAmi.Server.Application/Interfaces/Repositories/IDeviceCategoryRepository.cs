using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IDeviceCategoryRepository
    {
        Task<DeviceCategory?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<List<DeviceCategory>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken = default);
        Task<bool> HasDeviceTypesAsync(int categoryId, CancellationToken cancellationToken = default);
        Task AddAsync(DeviceCategory category, CancellationToken cancellationToken = default);
        void Update(DeviceCategory category);
        void Delete(DeviceCategory category);
    }
}
