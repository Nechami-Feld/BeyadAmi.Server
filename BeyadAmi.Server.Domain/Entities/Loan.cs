using System;

namespace BeyadAmi.Server.Domain.Entities
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int DeviceId { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public int DepositTypeId { get; set; }
        public DepositType? DepositType { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string? Notes { get; set; }

        // Navigation
        public Device? Device { get; set; }

        public Loan()
        {
            LoanDate = DateTime.UtcNow;
        }

        public bool IsActive => ReturnDate == null;
    }
}