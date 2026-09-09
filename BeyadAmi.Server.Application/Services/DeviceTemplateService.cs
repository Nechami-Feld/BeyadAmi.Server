using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.DTOs.DeviceTemplate;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class DeviceTemplateService : IDeviceTemplateService
    {
        private readonly IDeviceTemplateRepository _repository;
        private readonly CreateDeviceTemplateValidator _validator = new();

        public DeviceTemplateService(IDeviceTemplateRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateDeviceTemplateDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var entity = new DeviceTemplate
            {
                TemplateName = dto.TemplateName,
                TemplateText = dto.TemplateText
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.DeviceTemplateId;
        }

        public async Task UpdateAsync(int deviceTemplateId, UpdateDeviceTemplateDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(deviceTemplateId, cancellationToken)
                ?? throw new DeviceTemplateNotFoundException(deviceTemplateId);

            if (string.IsNullOrWhiteSpace(dto.TemplateName))
                throw new BusinessException("שם התבנית הוא שדה חובה.");

            if (string.IsNullOrWhiteSpace(dto.TemplateText))
                throw new BusinessException("תוכן התבנית הוא שדה חובה.");

            existing.TemplateName = dto.TemplateName;
            existing.TemplateText = dto.TemplateText;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int deviceTemplateId, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(deviceTemplateId, cancellationToken);
            if (existing == null) return;

            _repository.Delete(existing);
        }

        public async Task<DeviceTemplateDto?> GetByIdAsync(int deviceTemplateId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(deviceTemplateId, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<DeviceTemplateDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static DeviceTemplateDto MapToDto(DeviceTemplate c) => new()
        {
            TemplateName = c.TemplateName,
            TemplateText = c.TemplateText,
        };
    }
}
