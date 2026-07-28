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
                return new[] { "DeviceCategory payload is required." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.CategoryName))
                errors.Add("CategoryName is required.");

            return errors;
        }

        public bool IsValid(CreateDeviceCategoryDto dto) => !Validate(dto).Any();
    }
}
