using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Domain.Entities;
using BeyadAmi.Server.Infrastructure.Persistence;

namespace BeyadAmi.Server.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(userName)) return null;
            return await _db.Set<User>().AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == userName, cancellationToken);
        }
    }
}
