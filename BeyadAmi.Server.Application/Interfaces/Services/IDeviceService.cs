using System.Collections.Generic;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Device;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDeviceService
    {
        Task<int> CreateAsync(CreateDeviceDto dto);
        Task UpdateAsync(UpdateDeviceDto dto);
        Task DeleteAsync(int deviceId);
        Task<DeviceDto?> GetByIdAsync(int deviceId);
        Task<IEnumerable<DeviceDto>> GetAllAsync();
    }
}