using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class Store
    {
        public int StoreId { get; set; }
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public ICollection<StoreProduct>? StoreProducts { get; set; }
        public ICollection<Purchase>? Purchases { get; set; }

        public Store()
        {
            StoreProducts = new List<StoreProduct>();
            Purchases = new List<Purchase>();
        }
    }
}