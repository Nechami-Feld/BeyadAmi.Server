using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
    {
        public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
        {
            builder.ToTable("SurveyQuestions");

            builder.HasKey(sq => sq.QuestionId);

            builder.Property(sq => sq.QuestionText)
                .HasMaxLength(1000);

            builder.Property(sq => sq.OrderNumber)
                .IsRequired();

            builder.Property(sq => sq.IsActive)
                .IsRequired();

            builder.HasIndex(sq => sq.OrderNumber).HasDatabaseName("IX_SurveyQuestions_OrderNumber");
        }
    }
}
