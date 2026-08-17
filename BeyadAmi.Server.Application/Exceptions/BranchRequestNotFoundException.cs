namespace BeyadAmi.Server.Application.Exceptions
{
    public class BranchRequestNotFoundException : BusinessException
    {
        public BranchRequestNotFoundException(int requestId)
            : base($"בקשת סניף עם מזהה {requestId} לא נמצאה.")
        {
        }
    }
}
