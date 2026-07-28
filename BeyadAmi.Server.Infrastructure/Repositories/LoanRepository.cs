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
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _db;

        public LoanRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Loan?> GetByIdAsync(int loanId, CancellationToken cancellationToken = default)
        {
            return await _db.Loans
                .Include(l => l.Device)
                        .ThenInclude(dt => dt!.Category)
                .Include(l => l.Device)
                    .ThenInclude(d => d!.Branch)
                .Include(l => l.DepositType)
                .AsNoTracking()
                .FirstOrDefaultAsync(l => l.LoanId == loanId, cancellationToken);
        }

        public async Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Loans
                .Include(l => l.Device)
                        .ThenInclude(dt => dt!.Category)
                .Include(l => l.Device)
                    .ThenInclude(d => d!.Branch)
                .Include(l => l.DepositType)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Loan>> GetActiveAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Loans
                .Include(l => l.Device)
                        .ThenInclude(dt => dt!.Category)
                .Include(l => l.Device)
                    .ThenInclude(d => d!.Branch)
                .Include(l => l.DepositType)
                .AsNoTracking()
                .Where(l => l.ReturnDate == null)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Loan>> GetByDeviceAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            return await _db.Loans
                .Include(l => l.DepositType)
                .AsNoTracking()
                .Where(l => l.DeviceId == deviceId)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> HasActiveLoanAsync(int deviceId, CancellationToken cancellationToken = default)
        {
            return await _db.Loans
                .AnyAsync(l => l.DeviceId == deviceId && l.ReturnDate == null, cancellationToken);
        }

        public async Task AddAsync(Loan loan, CancellationToken cancellationToken = default)
        {
            await _db.Loans.AddAsync(loan, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Loan loan)
        {
            var tracked = _db.ChangeTracker.Entries<Loan>()
                .FirstOrDefault(e => e.Entity.LoanId == loan.LoanId);

            if (tracked == null)
            {
                _db.Loans.Attach(loan);
                _db.Entry(loan).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(loan);
            }

            _db.SaveChanges();
        }

        public void Delete(Loan loan)
        {
            var tracked = _db.ChangeTracker.Entries<Loan>()
                .FirstOrDefault(e => e.Entity.LoanId == loan.LoanId);

            if (tracked == null)
                _db.Loans.Attach(loan);

            _db.Loans.Remove(loan);
            _db.SaveChanges();
        }
    }
}
