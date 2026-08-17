namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceNotFoundException : BusinessException
    {
        public DeviceNotFoundException(int deviceId)
            : base($"מכשיר עם מזהה {deviceId} לא נמצא.")
        {
        }
    }
}
