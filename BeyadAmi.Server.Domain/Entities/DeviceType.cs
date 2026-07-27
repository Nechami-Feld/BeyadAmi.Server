using System;
using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class DeviceType
    {
        public int DeviceTypeId { get; set; }
        public int CategoryId { get; set; }
        public string? DeviceName { get; set; }
        public string? Company { get; set; }
        public string? Model { get; set; }
        public string? BasicInfo { get; set; }
        public string? Rules { get; set; }

        // Navigation
        public DeviceCategory? Category { get; set; }
        public ICollection<Device>? Devices { get; set; }
        public ICollection<DeviceTemplate>? DeviceTemplates { get; set; }

        public DeviceType()
        {
            Devices = new List<Device>();
            DeviceTemplates = new List<DeviceTemplate>();
        }
    }
}