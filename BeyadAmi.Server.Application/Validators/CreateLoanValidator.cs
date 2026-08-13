using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Loans;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateLoanValidator
    {
        private const int DepositTypeNoneId = 3;

        public IEnumerable<string> Validate(CreateLoanDto dto)
        {
            if (dto == null)
                return new[] { "Loan payload is required." };

            var errors = new List<string>();

            if (dto.DeviceId <= 0)
                errors.Add("DeviceId is required.");

            if (string.IsNullOrWhiteSpace(dto.BorrowerLastName))
                errors.Add("BorrowerLastName is required.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                errors.Add("Phone is required.");

            if (dto.DepositTypeId <= 0)
                errors.Add("DepositTypeId is required.");

            return errors;
        }

        public bool IsValid(CreateLoanDto dto) => !Validate(dto).Any();
    }
}
