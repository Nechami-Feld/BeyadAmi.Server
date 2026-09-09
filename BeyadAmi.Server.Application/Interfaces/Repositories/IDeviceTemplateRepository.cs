using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IDeviceTemplateRepository
    {
        Task<DeviceTemplate?> GetByIdAsync(int templateId, CancellationToken cancellationToken = default);
        Task<List<DeviceTemplate>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<DeviceTemplate>> SearchAsync(string? searchText, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(string templateName, string templateText, CancellationToken cancellationToken = default);
        Task<bool> HasDeviceTemplateAsync(int deviceTemplateId, CancellationToken cancellationToken = default);
        Task AddAsync(DeviceTemplate deviceTemplate, CancellationToken cancellationToken = default);
        void Update(DeviceTemplate deviceTemplate);
        void Delete(DeviceTemplate deviceTemplate);
    }
}
