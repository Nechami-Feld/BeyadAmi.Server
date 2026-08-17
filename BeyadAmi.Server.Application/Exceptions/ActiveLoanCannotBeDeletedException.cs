namespace BeyadAmi.Server.Application.Exceptions
{
    public class ActiveLoanCannotBeDeletedException : BusinessException
    {
        public ActiveLoanCannotBeDeletedException(int loanId)
            : base($"השאלה עם מזהה {loanId} פעילה ולא ניתן למחוק אותה.")
        {
        }
    }
}
