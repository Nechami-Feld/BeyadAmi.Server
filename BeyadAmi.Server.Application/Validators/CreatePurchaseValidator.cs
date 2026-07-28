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
                return new[] { "Purchase payload is required." };

            var errors = new List<string>();

            if (dto.StoreId <= 0)
                errors.Add("StoreId is required.");

            if (dto.ProductId <= 0)
                errors.Add("ProductId is required.");

            if (dto.Quantity <= 0)
                errors.Add("Quantity must be greater than 0.");

            if (dto.PricePerUnit < 0)
                errors.Add("PricePerUnit must be greater than or equal to 0.");

            if (dto.PurchasedBy != null && dto.PurchasedBy.Length > 100)
                errors.Add("PurchasedBy must not exceed 100 characters.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("Notes must not exceed 500 characters.");

            return errors;
        }

        public bool IsValid(CreatePurchaseDto dto) => !Validate(dto).Any();
    }
}
