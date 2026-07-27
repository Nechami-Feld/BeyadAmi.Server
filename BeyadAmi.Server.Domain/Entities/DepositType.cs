using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class DepositType
    {
        public int DepositTypeId { get; set; }
        public string DepositTypeName { get; set; }

        // Navigation
        public ICollection<Loan>? Loans { get; set; }

        public DepositType()
        {
            DepositTypeName = string.Empty;
            Loans = new List<Loan>();
        }
    }
}
