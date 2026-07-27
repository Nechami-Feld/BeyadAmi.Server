using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class SurveySendingConfiguration : IEntityTypeConfiguration<SurveySending>
    {
        public void Configure(EntityTypeBuilder<SurveySending> builder)
        {
            builder.ToTable("SurveySendings");

            builder.HasKey(ss => ss.SurveySendId);

            builder.Property(ss => ss.SendDate)
                .IsRequired();

            builder.Property(ss => ss.Token)
                .HasMaxLength(250);

            builder.Property(ss => ss.IsAnswered)
                .IsRequired();

            builder.HasOne(ss => ss.Branch)
                .WithMany(b => b.SurveySendings)
                .HasForeignKey(ss => ss.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(ss => ss.Answers)
                .WithOne(a => a.SurveySending)
                .HasForeignKey(a => a.SurveySendId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(ss => ss.BranchId).HasDatabaseName("IX_SurveySendings_BranchId");
        }
    }
}
