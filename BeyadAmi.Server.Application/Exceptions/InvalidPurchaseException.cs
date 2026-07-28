namespace BeyadAmi.Server.Application.Exceptions
{
    public class InvalidPurchaseException : BusinessException
    {
        public InvalidPurchaseException(string message)
            : base(message)
        {
        }
    }
}
