using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.DepositType;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class DepositTypeService : IDepositTypeService
    {
        private readonly IDepositTypeRepository _repository;
        private readonly CreateDeviceCategoryValidator _validator = new();

        public DepositTypeService(IDepositTypeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<List<DepositTypeDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static DepositTypeDto MapToDto(DepositType c) => new()
        {
            DepositTypeId = c.DepositTypeId,
            DepositTypeName = c.DepositTypeName,
        };
    }
}
