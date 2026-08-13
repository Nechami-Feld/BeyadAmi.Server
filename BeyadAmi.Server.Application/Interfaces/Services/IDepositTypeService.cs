using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DepositType;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface IDepositTypeService
    {
        Task<List<DepositTypeDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
