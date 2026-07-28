namespace BeyadAmi.Server.Application.Exceptions
{
    public class InvalidDepositException : BusinessException
    {
        public InvalidDepositException()
            : base("DepositAmount is required when DepositType is not None.")
        {
        }
    }
}
