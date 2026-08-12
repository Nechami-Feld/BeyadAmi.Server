
using System;

namespace BeyadAmi.Server.Application.DTOs.BranchRequests
{
    public class CreateBranchRequestDto
    {
        public int BranchId { get; set; }
        public string? Request { get; set; }
        public string? Notes { get; set; }
    }
}
