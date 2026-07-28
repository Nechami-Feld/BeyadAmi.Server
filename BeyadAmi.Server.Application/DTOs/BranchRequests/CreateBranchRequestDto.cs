using System;

namespace BeyadAmi.Server.Application.DTOs.BranchRequests
{
    public class CreateBranchRequestDto
    {
        public int BranchId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? Notes { get; set; }
    }
}
