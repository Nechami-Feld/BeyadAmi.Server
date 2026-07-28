using System;

namespace BeyadAmi.Server.Application.DTOs.BranchRequests
{
    public class UpdateBranchRequestDto
    {
        public bool IsCompleted { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string? Notes { get; set; }
    }
}
