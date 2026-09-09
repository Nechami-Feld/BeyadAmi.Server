using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.DTOs.DeviceTemplate;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDeviceTemplateService
    {
        Task<DeviceTemplateDto?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<List<DeviceTemplateDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateDeviceTemplateDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int categoryId, UpdateDeviceTemplateDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
