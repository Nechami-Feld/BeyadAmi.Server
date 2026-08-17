namespace BeyadAmi.Server.Application.Exceptions
{
    public class LoanNotFoundException : BusinessException
    {
        public LoanNotFoundException(int loanId)
            : base($"השאלה עם מזהה {loanId} לא נמצאה.")
        {
        }
    }
}
