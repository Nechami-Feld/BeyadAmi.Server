using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Purchases;

namespace BeyadAmi.Server.Application.Validators
{
    public class CreatePurchaseValidator
    {
        public IEnumerable<string> Validate(CreatePurchaseDto dto)
        {
            if (dto == null)
                return new[] { "נדרש מידע לרכישה." };

            var errors = new List<string>();

            if (dto.StoreId <= 0)
                errors.Add("נדרש מזהה חנות.");

            if (dto.ProductId <= 0)
                errors.Add("נדרש מזהה מוצר.");

            if (dto.Quantity <= 0)
                errors.Add("הכמות חייבת להיות גדולה מ-0.");

            if (dto.PricePerUnit < 0)
                errors.Add("מחיר ליחידה חייב להיות גדול מ-0 או שווה לו.");

            if (dto.PurchasedBy != null && dto.PurchasedBy.Length > 100)
                errors.Add("שם הרוכש לא יכול לעלות על 100 תווים.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("ההערות לא יכולות לעלות על 500 תווים.");

            return errors;
        }

        public bool IsValid(CreatePurchaseDto dto) => !Validate(dto).Any();
    }
}
