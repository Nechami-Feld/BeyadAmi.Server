namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceHasActiveLoanException : BusinessException
    {
        public DeviceHasActiveLoanException(int deviceId)
            : base($"Device with id {deviceId} cannot be deleted because it has an active loan.")
        {
        }
    }
}
