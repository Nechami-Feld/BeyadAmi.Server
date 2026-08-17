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
                return new[] { "נדרש מידע להשאלה." };

            var errors = new List<string>();

            if (dto.DeviceId <= 0)
                errors.Add("נדרש מזהה מכשיר.");

            if (string.IsNullOrWhiteSpace(dto.BorrowerLastName))
                errors.Add("שם משפחת השואל הוא שדה חובה.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                errors.Add("מספר הטלפון הוא שדה חובה.");

            if (dto.DepositTypeId <= 0)
                errors.Add("נדרש מזהה סוג פיקדון.");

            return errors;
        }

        public bool IsValid(CreateLoanDto dto) => !Validate(dto).Any();
    }
}
