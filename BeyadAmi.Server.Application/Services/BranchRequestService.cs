using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BeyadAmi.Server.Application.DTOs.BranchRequests;
using BeyadAmi.Server.Application.Exceptions;
using BeyadAmi.Server.Application.Interfaces.Repositories;
using BeyadAmi.Server.Application.Interfaces.Services;
using BeyadAmi.Server.Application.Validators;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Application.Services
{
    public class BranchRequestService : IBranchRequestService
    {
        private readonly IBranchRequestRepository _repository;
        private readonly CreateBranchRequestValidator _validator = new();

        public BranchRequestService(IBranchRequestRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<int> CreateAsync(CreateBranchRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var errors = _validator.Validate(dto).ToList();
            if (errors.Any())
                throw new BusinessException(string.Join(" ", errors));

            var entity = new BranchRequest
            {
                BranchId = dto.BranchId,
                RequestDate = dto.RequestDate ?? DateTime.UtcNow,
                Notes = dto.Notes,
                IsCompleted = false
            };

            await _repository.AddAsync(entity, cancellationToken);
            return entity.RequestId;
        }

        public async Task CompleteAsync(int id, UpdateBranchRequestDto dto, CancellationToken cancellationToken = default)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var existing = await _repository.GetByIdAsync(id, cancellationToken)
                ?? throw new BranchRequestNotFoundException(id);

            if (dto.IsCompleted && dto.CompletedDate == null)
                throw new BusinessException("CompletedDate is required when IsCompleted is true.");

            if (!dto.IsCompleted && dto.CompletedDate != null)
                throw new BusinessException("CompletedDate can only be set when IsCompleted is true.");

            existing.IsCompleted = dto.IsCompleted;
            existing.CompletedDate = dto.IsCompleted ? dto.CompletedDate : null;
            existing.Notes = dto.Notes;

            _repository.Update(existing);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(id, cancellationToken);
            if (existing == null) return;

            _repository.Delete(existing);
        }

        public async Task<BranchRequestDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(id, cancellationToken);
            return entity == null ? null : MapToDto(entity);
        }

        public async Task<List<BranchRequestDto>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetAllAsync(cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        public async Task<List<BranchRequestDto>> GetByBranchAsync(int branchId, CancellationToken cancellationToken = default)
        {
            var all = await _repository.GetByBranchAsync(branchId, cancellationToken);
            return all.Select(MapToDto).ToList();
        }

        private static BranchRequestDto MapToDto(BranchRequest r) => new()
        {
            RequestId = r.RequestId,
            BranchId = r.BranchId,
            BranchName = r.Branch?.BranchName,
            RequestDate = r.RequestDate,
            IsCompleted = r.IsCompleted,
            CompletedDate = r.CompletedDate,
            Notes = r.Notes
        };
    }
}
