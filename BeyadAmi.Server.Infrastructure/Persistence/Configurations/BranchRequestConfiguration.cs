using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class BranchRequestConfiguration : IEntityTypeConfiguration<BranchRequest>
    {
        public void Configure(EntityTypeBuilder<BranchRequest> builder)
        {
            builder.ToTable("BranchRequests");

            // Primary Key
            builder.HasKey(br => br.RequestId);

            // Properties

            builder.Property(br => br.Request)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(br => br.RequestDate)
                .IsRequired();

            builder.Property(br => br.Description)
                .HasMaxLength(1000);

            builder.Property(br => br.IsCompleted)
                .IsRequired();

            builder.Property(br => br.Notes)
                .HasColumnType("text");
            // Relationships
            builder.HasOne(br => br.Branch)
                .WithMany(b => b.BranchRequests)
                .HasForeignKey(br => br.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(br => br.BranchId).HasDatabaseName("IX_BranchRequests_BranchId");
            builder.HasIndex(br => br.RequestDate).HasDatabaseName("IX_BranchRequests_RequestDate");
        }
    }
}
