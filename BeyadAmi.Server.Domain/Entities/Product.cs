using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }

        // Navigation
        public ICollection<StoreProduct>? StoreProducts { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }

        public Product()
        {
            StoreProducts = new List<StoreProduct>();
            Purchases = new List<Purchase>();
        }
    }
}