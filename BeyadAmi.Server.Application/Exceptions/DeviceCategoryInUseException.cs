namespace BeyadAmi.Server.Application.Exceptions
{
    public class DeviceCategoryInUseException : BusinessException
    {
        public DeviceCategoryInUseException(int categoryId)
            : base($"קטגוריית מכשיר עם מזהה {categoryId} לא ניתנת למחיקה כי יש לה סוגי מכשירים משויכים.")
        {
        }
    }
}
