using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.HasKey(b => b.BranchId);

            builder.Property(b => b.BranchName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.City)
                .HasMaxLength(100);

            builder.Property(b => b.Street)
                .HasMaxLength(100);

            builder.Property(b => b.Apartment)
                .HasMaxLength(20);

           

            builder.Property(b => b.Phone)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(b => b.Email)
                .HasMaxLength(150);

            builder.Property(b => b.Notes)
                .HasColumnType("text");

            builder.Property(b => b.IsActive)
                .IsRequired();

            // Relationships
            builder.HasMany(b => b.Devices)
                .WithOne(d => d.Branch)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(b => b.BranchRequests)
                .WithOne(r => r.Branch)
                .HasForeignKey(r => r.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(b => b.SurveySendings)
                .WithOne(s => s.Branch)
                .HasForeignKey(s => s.BranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Indexes
            builder.HasIndex(b => b.BranchName).HasDatabaseName("IX_Branches_BranchName");
        }
    }
}
