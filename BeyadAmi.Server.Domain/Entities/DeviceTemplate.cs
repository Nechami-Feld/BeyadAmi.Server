using System;

namespace BeyadAmi.Server.Domain.Entities
{
    public class DeviceTemplate
    {
        public int TemplateId { get; set; }
        public int DeviceTypeId { get; set; }
        public string? TemplateName { get; set; }
        public string? FilePath { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation
        public DeviceType? DeviceType { get; set; }

        public DeviceTemplate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}