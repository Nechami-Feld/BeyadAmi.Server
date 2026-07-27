using System;
using System.Collections.Generic;

namespace BeyadAmi.Server.Domain.Entities
{
    public class SurveySending
    {
        public int SurveySendId { get; set; }
        public int BranchId { get; set; }
        public DateTime SendDate { get; set; }
        public string? Token { get; set; }
        public bool IsAnswered { get; set; }

        // Navigation
        public Branch? Branch { get; set; }
        public ICollection<SurveyAnswer>? Answers { get; set; }

        public SurveySending()
        {
            Answers = new List<SurveyAnswer>();
            SendDate = DateTime.UtcNow;
        }
    }
}