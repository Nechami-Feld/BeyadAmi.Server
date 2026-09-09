using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.DeviceCategory;
using BeyadAmi.Server.Application.DTOs.DeviceTemplate;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateDeviceTemplateValidator
    {
        public IEnumerable<string> Validate(CreateDeviceTemplateDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע לתבנית מכשיר." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.TemplateName))
                errors.Add("שם התבנית הוא שדה חובה.");

            if (string.IsNullOrWhiteSpace(dto.TemplateText))
                errors.Add("תוכן התבנית הוא שדה חובה.");

            return errors;
        }

        public bool IsValid(CreateDeviceTemplateDto dto) => !Validate(dto).Any();
    }
}
