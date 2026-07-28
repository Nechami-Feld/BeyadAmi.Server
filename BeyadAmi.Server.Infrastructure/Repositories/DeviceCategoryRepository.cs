using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class DeviceCategoryRepository : IDeviceCategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public DeviceCategoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DeviceCategory?> GetByIdAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceCategories
                .Include(c => c.DeviceTypes)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == categoryId, cancellationToken);
        }

        public async Task<List<DeviceCategory>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.DeviceCategories
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceCategories
                .AnyAsync(c => c.CategoryId == categoryId, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceCategories
                .AnyAsync(c => c.CategoryName == categoryName, cancellationToken);
        }

        public async Task<bool> HasDeviceTypesAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            return await _db.DeviceTypes
                .AnyAsync(t => t.CategoryId == categoryId, cancellationToken);
        }

        public async Task AddAsync(DeviceCategory category, CancellationToken cancellationToken = default)
        {
            await _db.DeviceCategories.AddAsync(category, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(DeviceCategory category)
        {
            var tracked = _db.ChangeTracker.Entries<DeviceCategory>()
                .FirstOrDefault(e => e.Entity.CategoryId == category.CategoryId);

            if (tracked == null)
            {
                _db.DeviceCategories.Attach(category);
                _db.Entry(category).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(category);
            }

            _db.SaveChanges();
        }

        public void Delete(DeviceCategory category)
        {
            var tracked = _db.ChangeTracker.Entries<DeviceCategory>()
                .FirstOrDefault(e => e.Entity.CategoryId == category.CategoryId);

            if (tracked == null)
                _db.DeviceCategories.Attach(category);

            _db.DeviceCategories.Remove(category);
            _db.SaveChanges();
        }
    }
}
