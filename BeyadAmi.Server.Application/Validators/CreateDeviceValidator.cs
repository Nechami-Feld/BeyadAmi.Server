using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Device;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateDeviceValidator
    {
        public IEnumerable<string> Validate(CreateDeviceDto dto)
        {
            if (dto == null)
            {
                return new[] { "נדרש מידע למכשיר." };
            }

            var errors = new List<string>();

            if (dto.CategoryId <= 0)
                errors.Add("מזהה הקטגוריה חייב להיות מספר חיובי.");

            if (dto.BranchId <= 0)
                errors.Add("מזהה הסניף חייב להיות מספר חיובי.");

            if (string.IsNullOrWhiteSpace(dto.DeviceNumber))
                errors.Add("מספר המכשיר הוא שדה חובה.");
            else if (dto.DeviceNumber.Length > 50)
                errors.Add("מספר המכשיר לא יכול לעלות על 50 תווים.");

            if (!string.IsNullOrWhiteSpace(dto.Company) && dto.Company.Length > 100)
                errors.Add("שם החברה לא יכול לעלות על 100 תווים.");

            return errors;
        }

        public bool IsValid(CreateDeviceDto dto) => !Validate(dto).Any();
    }
}
