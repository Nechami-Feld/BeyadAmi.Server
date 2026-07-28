using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.BranchRequests;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateBranchRequestValidator
    {
        public IEnumerable<string> Validate(CreateBranchRequestDto dto)
        {
            if (dto == null)
                return new[] { "BranchRequest payload is required." };

            var errors = new List<string>();

            if (dto.BranchId <= 0)
                errors.Add("BranchId is required.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("Notes must not exceed 500 characters.");

            return errors;
        }

        public bool IsValid(CreateBranchRequestDto dto) => !Validate(dto).Any();
    }
}
