using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class StoreNotFoundException : BusinessException
    {
        public StoreNotFoundException(int storeId) : base($"חנות עם מזהה {storeId} לא נמצאה.") { }
    }
}
