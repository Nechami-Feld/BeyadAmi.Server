using System.Collections.Generic;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Loans;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface ILoanService
    {
        Task<LoanDto?> GetByIdAsync(int loanId, CancellationToken cancellationToken = default);
        Task<List<LoanDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<LoanDto>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<List<LoanDto>> GetByDeviceAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateLoanDto dto, CancellationToken cancellationToken = default);
        Task ReturnAsync(int loanId, ReturnLoanDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int loanId, UpdateLoanDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int loanId, CancellationToken cancellationToken = default);
    }

}
