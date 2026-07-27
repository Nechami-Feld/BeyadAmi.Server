namespace BeyadAmi.Server.Application.DTOs.Branches
{
    public class BranchDto
    {
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? Apartment { get; set; }
        public string? ManagerLastName { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public int DevicesCount { get; set; }
    }
}
