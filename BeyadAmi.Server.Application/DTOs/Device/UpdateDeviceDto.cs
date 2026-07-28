namespace BeyadAmi.Server.Application.DTOs.Device
{
    public class UpdateDeviceDto
    {
        public int CategoryId { get; set; }
        public int BranchId { get; set; }
        public string? Company { get; set; }
        public string? Notes { get; set; }
    }
}