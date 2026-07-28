namespace BeyadAmi.Server.Application.Exceptions
{
    public class LoanNotFoundException : BusinessException
    {
        public LoanNotFoundException(int loanId)
            : base($"Loan with id {loanId} was not found.")
        {
        }
    }
}
