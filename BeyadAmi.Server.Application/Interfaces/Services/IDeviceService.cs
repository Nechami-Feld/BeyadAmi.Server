using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Device;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<DeviceDto?> GetByIdAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<List<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<DeviceDto>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default);
        Task<List<DeviceDto>> GetAvailableAsync(int branchId, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateDeviceDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int deviceId, UpdateDeviceDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int deviceId, CancellationToken cancellationToken = default);
    }
}