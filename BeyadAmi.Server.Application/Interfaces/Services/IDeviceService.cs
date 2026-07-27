using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Device;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<int> CreateAsync(CreateDeviceDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(UpdateDeviceDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<DeviceDto?> GetByIdAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<IEnumerable<DeviceDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}