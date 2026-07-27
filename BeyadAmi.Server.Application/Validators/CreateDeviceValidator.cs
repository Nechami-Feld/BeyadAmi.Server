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

            if (dto.DeviceTypeId <= 0)
                errors.Add("DeviceTypeId must be a positive integer.");

            if (dto.BranchId <= 0)
                errors.Add("BranchId must be a positive integer.");

            if (!string.IsNullOrWhiteSpace(dto.DeviceNumber) && dto.DeviceNumber.Length > 100)
                errors.Add("DeviceNumber must be at most 100 characters.");

            if (!string.IsNullOrWhiteSpace(dto.Company) && dto.Company.Length > 100)
                errors.Add("Company must be at most 100 characters.");

            return errors;
        }

        public bool IsValid(CreateDeviceDto dto) => !Validate(dto).Any();
    }
}
