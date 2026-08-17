using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException() : base("משתמש כבר קיים.") { }
    }
}
