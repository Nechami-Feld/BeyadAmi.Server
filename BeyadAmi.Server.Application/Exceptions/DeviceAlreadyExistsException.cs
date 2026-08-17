namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceAlreadyExistsException : BusinessException
    {
        public DeviceAlreadyExistsException(string deviceNumber)
            : base($"מכשיר עם מספר '{deviceNumber}' כבר קיים.")
        {
        }
    }
}
