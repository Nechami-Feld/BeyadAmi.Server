namespace BeyadAmi.Server.Application.DTOs.Device
{
    public class CreateDeviceDto
    {
        public int CategoryId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public string? DeviceNumber { get; set; }
        public string? Notes { get; set; }
    }
}