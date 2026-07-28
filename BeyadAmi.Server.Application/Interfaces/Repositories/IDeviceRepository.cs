using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IDeviceRepository
    {
        Task<Device?> GetByIdAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<List<Device>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Device>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default);
        Task<List<Device>> GetAvailableAsync(int branchId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNumberAsync(string deviceNumber, CancellationToken cancellationToken = default);
        Task<bool> HasActiveLoansAsync(int deviceId, CancellationToken cancellationToken = default);
        Task AddAsync(Device device, CancellationToken cancellationToken = default);
        void Update(Device device);
        void Delete(Device device);
    }
}
