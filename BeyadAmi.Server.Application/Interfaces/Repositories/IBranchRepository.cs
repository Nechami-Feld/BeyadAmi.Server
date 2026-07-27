using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IBranchRepository
    {
        Task<Branch?> GetByIdAsync(int branchId, CancellationToken cancellationToken = default);
        Task<List<Branch>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(int branchId, CancellationToken cancellationToken = default);
        Task<bool> HasDevicesAsync(int branchId, CancellationToken cancellationToken = default);
        Task AddAsync(Branch branch, CancellationToken cancellationToken = default);
        void Update(Branch branch);
        void Delete(Branch branch);
    }
}
