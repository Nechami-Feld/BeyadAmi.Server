namespace BeyadAmi.Server.Application.DTOs.Products
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public string? Notes { get; set; }
        public int PurchasesCount { get; set; }
    }
}
