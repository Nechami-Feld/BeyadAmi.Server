namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceTemplateNotFoundException : BusinessException
    {
        public DeviceTemplateNotFoundException(int deviceTemplateId)
            : base($"תבנית עם מזהה {deviceTemplateId} לא נמצא.")
        {
        }
    }
}
