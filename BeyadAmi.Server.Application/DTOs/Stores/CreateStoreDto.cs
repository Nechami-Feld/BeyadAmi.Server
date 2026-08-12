namespace BeyadAmi.Server.Application.DTOs.Stores
{
    public class CreateStoreDto
    {
        public string? StoreName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
    }
}
