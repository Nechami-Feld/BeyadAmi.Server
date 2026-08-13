using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface ICompanyRepository
    {
        Task<Company?> GetByIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<Company>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int companyId, CancellationToken cancellationToken = default);
        Task<bool> ExistsByNameAsync(string companyName, CancellationToken cancellationToken = default);
        Task AddAsync(Company company, CancellationToken cancellationToken = default);
        void Update(Company company);
        void Delete(Company company);
    }
}
