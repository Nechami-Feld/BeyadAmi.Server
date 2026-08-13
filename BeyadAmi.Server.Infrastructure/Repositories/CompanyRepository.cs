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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Company?> GetByIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _db.Companies
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CompanyId == companyId, cancellationToken);
        }

        public async Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.Companies
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsAsync(int companyId, CancellationToken cancellationToken = default)
        {
            return await _db.Companies.AnyAsync(c => c.CompanyId == companyId, cancellationToken);
        }

        public async Task<bool> ExistsByNameAsync(string companyName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(companyName)) return false;
            return await _db.Companies.AnyAsync(c => c.CompanyName == companyName, cancellationToken);
        }

        public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            await _db.Companies.AddAsync(company, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public void Update(Company company)
        {
            var tracked = _db.ChangeTracker.Entries<Company>().FirstOrDefault(e => e.Entity.CompanyId == company.CompanyId);
            if (tracked == null)
            {
                _db.Companies.Attach(company);
                _db.Entry(company).State = EntityState.Modified;
            }
            else
            {
                tracked.CurrentValues.SetValues(company);
            }
            _db.SaveChanges();
        }

        public void Delete(Company company)
        {
            var tracked = _db.ChangeTracker.Entries<Company>().FirstOrDefault(e => e.Entity.CompanyId == company.CompanyId);
            if (tracked == null)
            {
                _db.Companies.Attach(company);
            }

            _db.Companies.Remove(company);
            _db.SaveChanges();
        }
    }
}
