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
    public class DepositTypeRepository : IDepositTypeRepository
    {
        private readonly ApplicationDbContext _db;

        public DepositTypeRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<List<DepositType>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _db.DepositTypes
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
