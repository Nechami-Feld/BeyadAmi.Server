using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class StoreHasProductsException : BusinessException
    {
        public StoreHasProductsException(string message) : base(message) { }
    }
}
