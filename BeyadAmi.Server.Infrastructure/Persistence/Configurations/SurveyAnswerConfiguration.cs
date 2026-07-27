using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
    {
        public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
        {
            builder.ToTable("SurveyAnswers");

            builder.HasKey(sa => sa.AnswerId);

            builder.Property(sa => sa.AnswerText)
                .HasMaxLength(2000);

            builder.HasOne(sa => sa.SurveySending)
                .WithMany(ss => ss.Answers)
                .HasForeignKey(sa => sa.SurveySendId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(sa => sa.Question)
                .WithMany()
                .HasForeignKey(sa => sa.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(sa => sa.SurveySendId).HasDatabaseName("IX_SurveyAnswers_SurveySendId");
            builder.HasIndex(sa => sa.QuestionId).HasDatabaseName("IX_SurveyAnswers_QuestionId");
        }
    }
}
