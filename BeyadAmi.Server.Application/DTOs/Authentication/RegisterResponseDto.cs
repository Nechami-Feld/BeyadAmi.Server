namespace BeyadAmi.Server.Application.DTOs.Authentication
{
    public class RegisterResponseDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
    }
}
