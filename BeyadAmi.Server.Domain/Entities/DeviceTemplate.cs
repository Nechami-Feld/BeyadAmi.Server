using System;

namespace BeyadAmi.Server.Domain.Entities
{
    public class DeviceTemplate
    {
        public int DeviceTemplateId { get; set; }
        public int DeviceCategoryId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateText { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation
        public DeviceCategory? DeviceCategory{ get; set; }

        public DeviceTemplate()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}