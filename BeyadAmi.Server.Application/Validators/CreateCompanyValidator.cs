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
            if (dto == null) return new[] { "נדרש מידע לחברה." };

            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(dto.CompanyName))
                errors.Add("שם החברה הוא שדה חובה.");
            else if (dto.CompanyName.Length > MaxNameLength)
                errors.Add($"שם החברה לא יכול לעלות על {MaxNameLength} תווים.");

            return errors;
        }

        public bool IsValid(CreateCompanyDto dto) => !Validate(dto).Any();
    }
}
