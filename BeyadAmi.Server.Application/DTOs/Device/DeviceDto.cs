namespace BeyadAmi.Server.Application.DTOs.Device
{
    public class DeviceDto
    {
        public int DeviceId { get; set; }
        public string? DeviceNumber { get; set; }
        public int DeviceTypeId { get; set; }
        public string? DeviceTypeName { get; set; }
        public string? CategoryName { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? Company { get; set; }
        public bool IsAvailable { get; set; }
        public string? Notes { get; set; }
    }
}