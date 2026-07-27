namespace BeyadAmi.Server.Domain.Entities
{
    public class RequiredProduct
    {
        public int RequiredProductId { get; set; }
        public string? ProductName { get; set; }
        public string? Model { get; set; }
        public string? Company { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
    }
}