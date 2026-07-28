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
    public class BranchRequestRepository : IBranchRequestRepository
    {
        private readonly ApplicationDbContext _db;

        public BranchRequestRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<BranchRequest?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default)
        {
            return await _db.BranchRequests
                .Include(r => r.Branch)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.RequestId == requestId, cancellationToken);
        }

        public async Task<List<BranchRequest>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.BranchRequests
                .Include(r => r.Branch)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<BranchRequest>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default)
        {
            return await _db.BranchRequests
                .Include(r => r.Branch)
                .AsNoTracking()
                .Where(r => r.BranchId == branchId)
                .ToListAsync(cancellationToken);
        }

        public async Task AddAsync(BranchRequest request, CancellationToken cancellationToken = default)
        {
            await _db.BranchRequests.AddAsync(request, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(BranchRequest request)
        {
            var tracked = _db.ChangeTracker.Entries<BranchRequest>()
                .FirstOrDefault(e => e.Entity.RequestId == request.RequestId);

            if (tracked == null)
            {
                _db.BranchRequests.Attach(request);
                _db.Entry(request).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(request);
            }

            _db.SaveChanges();
        }

        public void Delete(BranchRequest request)
        {
            var tracked = _db.ChangeTracker.Entries<BranchRequest>()
                .FirstOrDefault(e => e.Entity.RequestId == request.RequestId);

            if (tracked == null)
                _db.BranchRequests.Attach(request);

            _db.BranchRequests.Remove(request);
            _db.SaveChanges();
        }
    }
}
