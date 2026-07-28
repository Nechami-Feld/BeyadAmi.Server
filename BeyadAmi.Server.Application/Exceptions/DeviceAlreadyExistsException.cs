namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceAlreadyExistsException : BusinessException
    {
        public DeviceAlreadyExistsException(string deviceNumber)
            : base($"Device with number '{deviceNumber}' already exists.")
        {
        }
    }
}
