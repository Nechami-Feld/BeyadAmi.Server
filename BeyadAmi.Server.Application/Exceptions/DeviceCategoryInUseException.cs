namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceCategoryInUseException : BusinessException
    {
        public DeviceCategoryInUseException(int categoryId)
            : base($"DeviceCategory with id {categoryId} cannot be deleted because it has associated DeviceTypes.")
        {
        }
    }
}
