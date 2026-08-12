using System;
using System.Collections.Generic;
using System.Linq;
using BeyadAmi.Server.Application.DTOs.Authentication;

namespace BeyadAmi.Server.Application.Validators
{
    public class RegisterRequestValidator
    {
        public IEnumerable<string> Validate(RegisterRequestDto dto)
        {
            if (dto == null)
                return new[] { "Register payload is required." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.UserName))
                errors.Add("UserName is required.");
            else if (dto.UserName.Length > 100)
                errors.Add("UserName must not exceed 100 characters.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                errors.Add("Email is required.");
            else if (!IsValidEmail(dto.Email))
                errors.Add("Email is not a valid email address.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                errors.Add("Password is required.");
            else if (dto.Password.Length < 6)
                errors.Add("Password must be at least 6 characters long.");

            if (string.IsNullOrWhiteSpace(dto.ConfirmPassword))
                errors.Add("ConfirmPassword is required.");
            else if (dto.Password != dto.ConfirmPassword)
                errors.Add("Password and ConfirmPassword must match.");

            return errors;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool IsValid(RegisterRequestDto dto) => !Validate(dto).Any();
    }
}
