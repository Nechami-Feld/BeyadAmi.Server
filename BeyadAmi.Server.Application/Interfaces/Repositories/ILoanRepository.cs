using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface ILoanRepository
    {
        Task<Loan?> GetByIdAsync(int loanId, CancellationToken cancellationToken = default);
        Task<List<Loan>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<List<Loan>> GetActiveAsync(CancellationToken cancellationToken = default);
        Task<List<Loan>> GetByDeviceAsync(int deviceId, CancellationToken cancellationToken = default);
        Task<bool> HasActiveLoanAsync(int deviceId, CancellationToken cancellationToken = default);
        Task AddAsync(Loan loan, CancellationToken cancellationToken = default);
        void Update(Loan loan);
        void Delete(Loan loan);
    }
}
