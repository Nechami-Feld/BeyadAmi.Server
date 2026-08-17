using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class BranchNotFoundException : BusinessException
    {
        public BranchNotFoundException(int branchId)
            : base($"סניף עם מזהה {branchId} לא נמצא.")
        {
        }
    }
}
