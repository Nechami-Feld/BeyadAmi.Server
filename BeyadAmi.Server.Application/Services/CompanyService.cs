using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.Companies;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _repository;
        private readonly CreateCompanyValidator _validator = new();

        public CompanyService(ICompanyRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateCompanyDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            // check duplicate name
            if (await _repository.ExistsByNameAsync(dto.CompanyName, cancellationToken))
                throw new BusinessException("חברה עם אותו שם כבר קיימת.");

            var entity = new Company
            {
                CompanyName = dto.CompanyName
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.CompanyId;
        }

        public async Task UpdateAsync(int companyId, UpdateCompanyDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(companyId, cancellationToken);
            if (existing == null) throw new BusinessException($"חברה עם מזהה {companyId} לא נמצאה.");

            if (string.IsNullOrWhiteSpace(dto.CompanyName))
                throw new BusinessException("שם החברה הוא שדה חובה.");

            // if name changed, check duplicates
            if (!string.Equals(existing.CompanyName, dto.CompanyName, StringComparison.OrdinalIgnoreCase)
                && await _repository.ExistsByNameAsync(dto.CompanyName, cancellationToken))
            {
                throw new BusinessException("חברה עם אותו שם כבר קיימת.");
            }

            existing.CompanyName = dto.CompanyName;
            _repository.Update(existing);
        }

        public async Task DeleteAsync(int companyId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(companyId, cancellationToken);
            if (existing == null) return; // idempotent

            _repository.Delete(existing);
        }

        public async Task<CompanyDto?> GetByIdAsync(int companyId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(companyId, cancellationToken);
            if (entity == null) throw new BusinessException($"חברה עם מזהה {companyId} לא נמצאה.");

            return MapToDto(entity);
        }

        public async Task<List<CompanyDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static CompanyDto MapToDto(Company c) => new CompanyDto
        {
            CompanyId = c.CompanyId,
            CompanyName = c.CompanyName
        };
    }
}
