using System;
using System.Collections.Generic;
using System.Linq;

namespace BeyadAmi.Server.Domain.Entities
{
    public class Device
    {
        public int DeviceId { get; set; }
        public int DeviceTypeId { get; set; }
        public int BranchId { get; set; }
        public string? DeviceNumber { get; set; }
        public string? Company { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedDate { get; set; }

        // Navigation
        public DeviceType? DeviceType { get; set; }
        public Branch? Branch { get; set; }
        public ICollection<Loan>? Loans { get; set; }

        public Device()
        {
            Loans = new List<Loan>();
            CreatedDate = DateTime.UtcNow;
        }

        // Computed convenience property
        public bool IsLoaned => Loans != null && Loans.Any(l => l.ReturnDate == null);
    }
}