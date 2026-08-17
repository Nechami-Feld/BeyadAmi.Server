namespace BeyadAmi.Server.Application.Exceptions
{
    public class ProductNotFoundException : BusinessException
    {
        public ProductNotFoundException(int productId)
            : base($"מוצר עם מזהה {productId} לא נמצא.") { }
    }

    public class ProductAlreadyExistsException : BusinessException
    {
        public ProductAlreadyExistsException(string productName, string model, string? company)
            : base($"מוצר '{productName}' דגם '{model}' חברה '{company}' כבר קיים.") { }
    }

    public class ProductHasPurchasesException : BusinessException
    {
        public ProductHasPurchasesException(int productId)
            : base($"מוצר עם מזהה {productId} לא ניתן למחיקה כי יש לו רכישות קיימות.") { }
    }
}
