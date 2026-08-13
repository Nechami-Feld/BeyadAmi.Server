namespace BeyadAmi.Server.Application.DTOs.Loans
{
    public class UpdateLoanDto
    {
        public string? BorrowerLastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepositTypeId { get; set; }
        public string? Notes { get; set; }
    }
}
