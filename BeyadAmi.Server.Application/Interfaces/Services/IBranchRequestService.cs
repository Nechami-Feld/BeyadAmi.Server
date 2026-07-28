using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.BranchRequests;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IBranchRequestService
    {
        Task<BranchRequestDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<List<BranchRequestDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<BranchRequestDto>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateBranchRequestDto dto, CancellationToken cancellationToken = default);
        Task CompleteAsync(int id, UpdateBranchRequestDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
