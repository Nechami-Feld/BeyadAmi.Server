using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class DeviceTemplateRepository : IDeviceTemplateRepository
    {
        private readonly ApplicationDbContext _db;

        public DeviceTemplateRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DeviceTemplate?> GetByIdAsync(int deviceTemplateId, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceTemplates
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.DeviceTemplateId == deviceTemplateId, cancellationToken);
        }

        public async Task<List<DeviceTemplate>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.DeviceTemplates
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<DeviceTemplate>> SearchAsync(string? searchText, CancellationToken cancellationToken = default)
        {
            var query = _db.DeviceTemplates.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchText))
                query = query.Where(p =>
                    p.TemplateName!.Contains(searchText) ||
                    p.TemplateText!.Contains(searchText));

            return await query.ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(string templateName, string templateText, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceTemplates.AnyAsync(p =>
                p.TemplateName == templateName &&
                p.TemplateText == templateText, cancellationToken);
        }

        public async Task<bool> HasDeviceTemplateAsync(int deviceTemplateId, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceTemplates.AnyAsync(p => p.DeviceTemplateId == deviceTemplateId, cancellationToken);
        }

        public async Task AddAsync(DeviceTemplate deviceTemplate, CancellationToken cancellationToken = default)
        {
            await _db.DeviceTemplates.AddAsync(deviceTemplate, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(DeviceTemplate deviceTemplate)
        {
            var tracked = _db.ChangeTracker.Entries<DeviceTemplate>()
                .FirstOrDefault(e => e.Entity.DeviceTemplateId == deviceTemplate.DeviceTemplateId);

            if (tracked == null)
            {
                _db.DeviceTemplates.Attach(deviceTemplate);
                _db.Entry(deviceTemplate).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(deviceTemplate);
            }

            _db.SaveChanges();
        }

        public void Delete(DeviceTemplate deviceTemplate)
        {
            var tracked = _db.ChangeTracker.Entries<DeviceTemplate>()
                .FirstOrDefault(e => e.Entity.DeviceTemplateId == deviceTemplate.DeviceTemplateId);

            if (tracked == null)
                _db.DeviceTemplates.Attach(deviceTemplate);

            _db.DeviceTemplates.Remove(deviceTemplate);
            _db.SaveChanges();
        }
    }
}
