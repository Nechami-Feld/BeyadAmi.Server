using System;

namespace BeyadAmi.Server.Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Role of the user. Default is User.
        public UserRole Role { get; set; } = UserRole.User;
    }
}
