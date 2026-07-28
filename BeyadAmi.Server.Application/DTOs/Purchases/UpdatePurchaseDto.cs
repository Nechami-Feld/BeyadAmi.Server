namespace BeyadAmi.Server.Application.DTOs.Purchases
{
    public class UpdatePurchaseDto
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public string? PurchasedBy { get; set; }
        public string? Receipt { get; set; }
        public string? Notes { get; set; }
    }
}
