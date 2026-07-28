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
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ApplicationDbContext _db;

        public DeviceRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Device device, CancellationToken cancellationToken = default)
        {
            await _db.Devices.AddAsync(device, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Device device)
        {
            // Attach if not tracked
            var tracked = _db.ChangeTracker.Entries<Device>().FirstOrDefault(e => e.Entity.DeviceId == device.DeviceId);
            if (tracked == null)
            {
                _db.Devices.Attach(device);
                _db.Entry(device).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(device);
            }

            _db.SaveChanges();
        }

        public void Delete(Device device)
        {
            var tracked = _db.ChangeTracker.Entries<Device>().FirstOrDefault(e => e.Entity.DeviceId == device.DeviceId);
            if (tracked == null)
            {
                _db.Devices.Attach(device);
            }

            _db.Devices.Remove(device);
            _db.SaveChanges();
        }

        public async Task<Device?> GetByIdAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            return await _db.Devices
                .Include(d => d.Category)
                .Include(d => d.Branch)
                .Include(d => d.Loans)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.DeviceId == deviceId, cancellationToken);
        }
        public async Task<List<Device>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Devices
                .Include(d => d.Category)
                .Include(d => d.Branch)
                .Include(d => d.Loans)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Device>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.Devices
                .Include(d => d.Category)
                .Include(d => d.Branch)
                .Include(d => d.Loans)
                .AsNoTracking()
                .Where(d => d.BranchId == branchId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Device>> GetAvailableAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.Devices
                .Include(d => d.Loans)
                .Where(d => d.BranchId == branchId && (d.Loans == null || !d.Loans.Any(l => l.ReturnDate == null)))
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsByNumberAsync(string deviceNumber, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(deviceNumber)) return false;
            var normalized = deviceNumber.Trim();
            return await _db.Devices.AnyAsync(d => d.DeviceNumber != null && d.DeviceNumber == normalized, cancellationToken);
        }

        public async Task<bool> HasActiveLoansAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            return await _db.Loans.AnyAsync(l => l.DeviceId == deviceId && l.ReturnDate == null, cancellationToken);
        }
    }
}
