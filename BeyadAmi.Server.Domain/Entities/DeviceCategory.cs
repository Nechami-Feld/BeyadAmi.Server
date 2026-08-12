using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class DeviceCategory
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? Description { get; set; }

        // Navigation
        public ICollection<Device>? Devices { get; set; }

        public DeviceCategory()
        {
            Devices = new List<Device>();
        }
    }
}