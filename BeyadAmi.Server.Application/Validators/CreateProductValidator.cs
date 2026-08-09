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
                return new[] { "Product payload is required." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.ProductName))
                errors.Add("ProductName is required.");
            else if (dto.ProductName.Length > 100)
                errors.Add("ProductName must not exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.Model))
                errors.Add("Model is required.");
            else if (dto.Model.Length > 100)
                errors.Add("Model must not exceed 100 characters.");

            if (dto.Company != null && dto.Company.Length > 100)
                errors.Add("Company must not exceed 100 characters.");

            if (dto.Notes != null && dto.Notes.Length > 500)
                errors.Add("Notes must not exceed 500 characters.");

            return errors;
        }

        public bool IsValid(CreateProductDto dto) => !Validate(dto).Any();
    }
}
