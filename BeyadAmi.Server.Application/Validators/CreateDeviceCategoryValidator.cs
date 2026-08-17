using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateDeviceCategoryValidator
    {
        public IEnumerable<string> Validate(CreateDeviceCategoryDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע לקטגוריית מכשיר." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                errors.Add("שם הקטגוריה הוא שדה חובה.");

            return errors;
        }

        public bool IsValid(CreateDeviceCategoryDto dto) => !Validate(dto).Any();
    }
}
