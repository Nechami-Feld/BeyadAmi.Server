namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceAlreadyLoanedException : BusinessException
    {
        public DeviceAlreadyLoanedException(int deviceId)
            : base($"Device with id {deviceId} is already loaned.")
        {
        }
    }
}
