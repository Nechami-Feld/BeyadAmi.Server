namespace BeyadAmi.Server.Application.DTOs.Branches
{
    public class CreateBranchDto
    {
        public string? BranchName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Apartment { get; set; }
        public string? ManagerLastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
    }
}
