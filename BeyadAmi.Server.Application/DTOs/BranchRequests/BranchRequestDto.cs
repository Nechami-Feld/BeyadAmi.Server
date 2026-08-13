using System;

namespace BeyadAmi.Server.Application.DTOs.BranchRequests
{
    public class BranchRequestDto
    {
        public int RequestId { get; set; }
        public int BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string Request { get; set; }
        public DateTime RequestDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Notes { get; set; }
    }
}
