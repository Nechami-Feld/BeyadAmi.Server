namespace BeyadAmi.Server.Application.Exceptions
{
    public class PurchaseNotFoundException : BusinessException
    {
        public PurchaseNotFoundException(int purchaseId)
            : base($"רכישה עם מזהה {purchaseId} לא נמצאה.")
        {
        }
    }
}
