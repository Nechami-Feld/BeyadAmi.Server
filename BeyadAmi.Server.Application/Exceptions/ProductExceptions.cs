namespace BeyadAmi.Server.Application.Exceptions
{
    public class ProductNotFoundException : BusinessException
    {
        public ProductNotFoundException(int productId)
            : base($"Product with ID {productId} was not found.") { }
    }

    public class ProductAlreadyExistsException : BusinessException
    {
        public ProductAlreadyExistsException(string productName, string model, string? company)
            : base($"Product '{productName}' model '{model}' company '{company}' already exists.") { }
    }

    public class ProductHasPurchasesException : BusinessException
    {
        public ProductHasPurchasesException(int productId)
            : base($"Product with ID {productId} cannot be deleted because it has existing purchases.") { }
    }
}
