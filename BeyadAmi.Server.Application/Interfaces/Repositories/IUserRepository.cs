using System.Threading;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<bool> ExistsByUserNameAsync(string userName, CancellationToken cancellationToken = default);
        Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken = default);
        Task AddAsync(User user, CancellationToken cancellationToken = default);
    }
}
