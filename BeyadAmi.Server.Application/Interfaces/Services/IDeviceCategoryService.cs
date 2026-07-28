using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDeviceCategoryService
    {
        Task<DeviceCategoryDto?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default);
        Task<List<DeviceCategoryDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateDeviceCategoryDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int categoryId, UpdateDeviceCategoryDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int categoryId, CancellationToken cancellationToken = default);
    }
}
