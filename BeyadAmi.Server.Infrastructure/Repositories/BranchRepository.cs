using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly ApplicationDbContext _db;

        public BranchRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Branch?> GetByIdAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.Branches
                .Include(b => b.Devices)
                .AsNoTracking()
                .FirstOrDefaultAsync(b => b.BranchId == branchId, cancellationToken);
        }

        public async Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Branches
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.Branches.AnyAsync(b => b.BranchId == branchId, cancellationToken);
        }

        public async Task<bool> HasDevicesAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.Devices.AnyAsync(d => d.BranchId == branchId, cancellationToken);
        }

        public async Task AddAsync(Branch branch, CancellationToken cancellationToken = default)
        {
            await _db.Branches.AddAsync(branch, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Branch branch)
        {
            var tracked = _db.ChangeTracker.Entries<Branch>().FirstOrDefault(e => e.Entity.BranchId == branch.BranchId);
            if (tracked == null)
            {
                _db.Branches.Attach(branch);
                _db.Entry(branch).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(branch);
            }

            _db.SaveChanges();
        }

        public void Delete(Branch branch)
        {
            var tracked = _db.ChangeTracker.Entries<Branch>().FirstOrDefault(e => e.Entity.BranchId == branch.BranchId);
            if (tracked == null)
            {
                _db.Branches.Attach(branch);
            }

            _db.Branches.Remove(branch);
            _db.SaveChanges();
        }
    }
}
