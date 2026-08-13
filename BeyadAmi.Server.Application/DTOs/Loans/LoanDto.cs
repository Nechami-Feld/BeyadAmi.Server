using System;

namespace BeyadAmi.Server.Application.DTOs.Loans
{
    public class LoanDto
    {
        public int LoanId { get; set; }
        public int DeviceId { get; set; }
        public string? DeviceNumber { get; set; }
        public string? BranchName { get; set; }
        public string? BorrowerLastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepositTypeId { get; set; }
        public string? DepositTypeName { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsActive { get; set; }
        public string? Notes { get; set; }
    }
}
