using System;

namespace BeyadAmi.Server.Application.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException() : base("פרטי התחברות שגויים.") { }
    }
}
