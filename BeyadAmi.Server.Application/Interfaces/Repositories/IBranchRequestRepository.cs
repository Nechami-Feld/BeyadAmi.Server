using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IBranchRequestRepository
    {
        Task<BranchRequest?> GetByIdAsync(int requestId, CancellationToken cancellationToken = default);
        Task<List<BranchRequest>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<BranchRequest>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default);
        Task AddAsync(BranchRequest request, CancellationToken cancellationToken = default);
        void Update(BranchRequest request);
        void Delete(BranchRequest request);
    }
}
