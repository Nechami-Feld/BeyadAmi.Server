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
                return new[] { "Device payload is required." };
            }

            var errors = new List<string>();

            if (dto.CategoryId <= 0)
                errors.Add("CategoryId must be a positive integer.");

            if (dto.BranchId <= 0)
                errors.Add("BranchId must be a positive integer.");

            if (string.IsNullOrWhiteSpace(dto.DeviceNumber))
                errors.Add("DeviceNumber is required.");
            else if (dto.DeviceNumber.Length > 50)
                errors.Add("DeviceNumber must be at most 50 characters.");

            if (!string.IsNullOrWhiteSpace(dto.Company) && dto.Company.Length > 100)
                errors.Add("Company must be at most 100 characters.");

            return errors;
        }

        public bool IsValid(CreateDeviceDto dto) => !Validate(dto).Any();
    }
}
