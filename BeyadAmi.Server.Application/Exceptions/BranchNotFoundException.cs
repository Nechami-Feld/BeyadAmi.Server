using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class BranchNotFoundException : BusinessException
    {
        public BranchNotFoundException(int branchId)
            : base($"Branch with id {branchId} was not found.")
        {
        }
    }
}
