namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceNotFoundException : BusinessException
    {
        public DeviceNotFoundException(int deviceId)
            : base($"Device with id {deviceId} was not found.")
        {
        }
    }
}
