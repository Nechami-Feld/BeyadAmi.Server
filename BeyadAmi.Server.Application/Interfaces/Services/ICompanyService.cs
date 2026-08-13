using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Companies;

namespace BeyadAmi.Server.Application.Interfaces.Services
{
    public interface ICompanyService
    {
        Task<CompanyDto?> GetByIdAsync(int companyId, CancellationToken cancellationToken = default);
        Task<List<CompanyDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<int> CreateAsync(CreateCompanyDto dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(int companyId, UpdateCompanyDto dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int companyId, CancellationToken cancellationToken = default);
    }
}
