using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Interfaces.Repositories
{
    public interface IDepositTypeRepository
    {
        Task<List<DepositType>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
