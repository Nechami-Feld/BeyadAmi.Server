namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceCategoryNotFoundException : BusinessException
    {
        public DeviceCategoryNotFoundException(int categoryId)
            : base($"DeviceCategory with id {categoryId} was not found.")
        {
        }
    }
}
