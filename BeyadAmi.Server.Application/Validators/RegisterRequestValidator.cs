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
                return new[] { "נדרש מידע להרשמה." };

            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(dto.UserName))
                errors.Add("שם המשתמש הוא שדה חובה.");
            else if (dto.UserName.Length > 100)
                errors.Add("שם המשתמש לא יכול לעלות על 100 תווים.");

            if (string.IsNullOrWhiteSpace(dto.Email))
                errors.Add("כתובת האימייל היא שדה חובה.");
            else if (!IsValidEmail(dto.Email))
                errors.Add("כתובת האימייל אינה תקינה.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                errors.Add("הסיסמה היא שדה חובה.");
            else if (dto.Password.Length < 6)
                errors.Add("הסיסמה חייבת להכיל לפחות 6 תווים.");

            if (string.IsNullOrWhiteSpace(dto.ConfirmPassword))
                errors.Add("אישור הסיסמה הוא שדה חובה.");
            else if (dto.Password != dto.ConfirmPassword)
                errors.Add("הסיסמה ואישור הסיסמה אינם תואמים.");

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
