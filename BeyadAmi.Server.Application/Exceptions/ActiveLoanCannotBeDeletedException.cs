namespace BeyadAmi.Server.Application.Exceptions
{
    public class ActiveLoanCannotBeDeletedException : BusinessException
    {
        public ActiveLoanCannotBeDeletedException(int loanId)
            : base($"Loan with id {loanId} is active and cannot be deleted.")
        {
        }
    }
}
