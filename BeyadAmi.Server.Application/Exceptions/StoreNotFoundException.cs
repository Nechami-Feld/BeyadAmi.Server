using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class StoreNotFoundException : BusinessException
    {
        public StoreNotFoundException(int storeId) : base($"Store with id {storeId} was not found.") { }
    }
}
