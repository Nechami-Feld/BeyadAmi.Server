using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.BranchRequests;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateBranchRequestValidator
    {
        public IEnumerable<string> Validate(CreateBranchRequestDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע לבקשת סניף." };

            var errors = new List<string>();

            if (dto.BranchId <= 0)
                errors.Add("נדרש מזהה סניף.");

            if (dto.Request is null)
                errors.Add("נדרש תוכן הבקשה.");

            if (dto.Request != null && dto.Request.Length > 500)
                errors.Add("הבקשה לא יכולה לעלות על 500 תווים.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("ההערות לא יכולות לעלות על 500 תווים.");

            return errors;
        }

        public bool IsValid(CreateBranchRequestDto dto) => !Validate(dto).Any();
    }
}
