using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Companies;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateCompanyValidator
    {
        private const int MaxNameLength = 200;

        public IEnumerable<string> Validate(CreateCompanyDto dto)
        {
            if (dto == null) return new[] { "CreateCompany payload is required." };

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(dto.CompanyName))
                errors.Add("CompanyName is required.");
            else if (dto.CompanyName.Length > MaxNameLength)
                errors.Add($"CompanyName must not exceed {MaxNameLength} characters.");

            return errors;
        }

        public bool IsValid(CreateCompanyDto dto) => !Validate(dto).Any();
    }
}
