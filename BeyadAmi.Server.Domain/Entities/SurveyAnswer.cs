namespace BeyadAmi.Server.Domain.Entities
{
    public class SurveyAnswer
    {
        public int AnswerId { get; set; }
        public int SurveySendId { get; set; }
        public int QuestionId { get; set; }

        // Navigation
        public SurveySending? SurveySending { get; set; }
        public SurveyQuestion? Question { get; set; }

        // Optionally store answer value/text in future
        public string? AnswerText { get; set; }
    }
}