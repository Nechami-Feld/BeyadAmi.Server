namespace BeyadAmi.Server.Domain.Entities
{
    public class StoreProduct
    {
        public int StoreProductId { get; set; }
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        // Navigation
        public Store? Store { get; set; }
        public Product? Product { get; set; }
    }
}