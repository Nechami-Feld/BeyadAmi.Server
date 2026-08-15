namespace BeyadAmi.Server.Application.DTOs.Stores
{
    public class UpdateStoreDto
    {
        public string? StoreName { get; set; }
        public bool? IsActive { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
    }
}
