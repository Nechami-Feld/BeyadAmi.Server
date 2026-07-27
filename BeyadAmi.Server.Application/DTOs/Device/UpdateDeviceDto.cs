namespace BeyadAmi.Server.Application.DTOs.Device
{
    public class UpdateDeviceDto
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public int BranchId { get; set; }
        public string? DeviceNumber { get; set; }
        public string? Company { get; set; }
        public string? Notes { get; set; }
    }
}