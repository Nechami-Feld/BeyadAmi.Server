using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class BranchHasDevicesException : BusinessException
    {
        public BranchHasDevicesException(string message)
            : base(message)
        {
        }
    }
}
