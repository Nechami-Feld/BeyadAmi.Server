namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceAlreadyLoanedException : BusinessException
    {
        public DeviceAlreadyLoanedException(int deviceId)
            : base($"מכשיר עם מזהה {deviceId} כבר מושאל.")
        {
        }
    }
}
