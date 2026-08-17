namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceHasActiveLoanException : BusinessException
    {
        public DeviceHasActiveLoanException(int deviceId)
            : base($"מכשיר עם מזהה {deviceId} לא ניתן למחיקה כי יש לו השאלה פעילה.")
        {
        }
    }
}
