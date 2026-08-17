using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using BeyadAmi.Server.Application.DTOs.Stores;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateStoreValidator
    {
        private static readonly Regex PhoneRegex = new(@"^[0-9+\-\s()]{7,20}$", RegexOptions.Compiled);
        private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

        public IEnumerable<string> Validate(CreateStoreDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע לחנות." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.StoreName))
                errors.Add("שם החנות הוא שדה חובה.");

            if (!string.IsNullOrWhiteSpace(dto.StoreName) && dto.StoreName.Length > 100)
                errors.Add("שם החנות לא יכול לעלות על 100 תווים.");

            if (!string.IsNullOrWhiteSpace(dto.Phone) && !PhoneRegex.IsMatch(dto.Phone))
                errors.Add("מספר הטלפון אינו תקין.");

            if (!string.IsNullOrWhiteSpace(dto.Email) && !EmailRegex.IsMatch(dto.Email))
                errors.Add("כתובת האימייל אינה תקינה.");

            return errors;
        }

        public bool IsValid(CreateStoreDto dto) => !Validate(dto).Any();
    }
}
