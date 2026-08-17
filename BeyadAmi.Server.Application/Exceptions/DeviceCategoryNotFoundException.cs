namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceCategoryNotFoundException : BusinessException
    {
        public DeviceCategoryNotFoundException(int categoryId)
            : base($"קטגוריית מכשיר עם מזהה {categoryId} לא נמצאה.")
        {
        }
    }
}
