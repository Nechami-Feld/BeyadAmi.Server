using System;

namespace BeyadAmi.Server.Application.DTOs.Purchases
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }
        public int StoreId { get; set; }
        public string? StoreName { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? ProductModel { get; set; }
        public string? ProductCompany { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal TotalPrice { get; set; }
        public string? PurchasedBy { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string? Receipt { get; set; }
        public string? Notes { get; set; }
    }
}
