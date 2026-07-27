namespace BeyadAmi.Server.Domain.Entities
{
    public class SurveyQuestion
    {
        public int QuestionId { get; set; }
        public string? QuestionText { get; set; }
        public int OrderNumber { get; set; }
        public bool IsActive { get; set; }
    }
}