namespace BeyadAmi.Server.Application.Exceptions
{
    public class BranchRequestNotFoundException : BusinessException
    {
        public BranchRequestNotFoundException(int requestId)
            : base($"BranchRequest with id {requestId} was not found.")
        {
        }
    }
}
