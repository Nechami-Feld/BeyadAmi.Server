using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Branches;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IBranchService
    {
        Task<BranchDto?> GetByIdAsync(int branchId, CancellationToken cancellationToken = default);
        Task<List<BranchDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateBranchDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int branchId, UpdateBranchDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int branchId, CancellationToken cancellationToken = default);
    }
}
