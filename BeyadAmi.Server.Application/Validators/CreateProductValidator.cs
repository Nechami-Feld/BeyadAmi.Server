using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Products;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreateProductValidator
    {
        public IEnumerable<string> Validate(CreateProductDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע למוצר." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.ProductName))
                errors.Add("שם המוצר הוא שדה חובה.");
            else if (dto.ProductName.Length > 100)
                errors.Add("שם המוצר לא יכול לעלות על 100 תווים.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                errors.Add("הדגם הוא שדה חובה.");
            else if (dto.Model.Length > 100)
                errors.Add("הדגם לא יכול לעלות על 100 תווים.");

            if (dto.Company != null && dto.Company.Length > 100)
                errors.Add("שם החברה לא יכול לעלות על 100 תווים.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("ההערות לא יכולות לעלות על 500 תווים.");

            return errors;
        }

        public bool IsValid(CreateProductDto dto) => !Validate(dto).Any();
    }
}
