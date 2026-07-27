using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BeyadAmi.Server.Application.DTOs.Branches;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateBranchValidator
    {
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
        private static readonly Regex PhoneRegex = new(@"^[0-9+\-\s()]{7,20}$", RegexOptions.Compiled);

        public IEnumerable<string> Validate(CreateBranchDto dto)
        {
            if (dto == null)
                return new[] { "Branch payload is required." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.BranchName))
                errors.Add("BranchName is required.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && !EmailRegex.IsMatch(dto.Email))
                errors.Add("Email format is invalid.");

            if (!string.IsNullOrWhiteSpace(dto.Phone) && !PhoneRegex.IsMatch(dto.Phone))
                errors.Add("Phone format is invalid.");

            return errors;
        }

        public bool IsValid(CreateBranchDto dto) => !Validate(dto).Any();
    }
}
