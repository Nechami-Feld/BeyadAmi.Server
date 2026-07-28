namespace BeyadAmi.Server.Application.Exceptions
{
    public class PurchaseNotFoundException : BusinessException
    {
        public PurchaseNotFoundException(int purchaseId)
            : base($"Purchase with id {purchaseId} was not found.")
        {
        }
    }
}
