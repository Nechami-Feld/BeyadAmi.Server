using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.ToTable("Loans");

            builder.HasKey(l => l.LoanId);

            builder.Property(l => l.FirstName)
                .HasMaxLength(100);

            builder.Property(l => l.LastName)
                .HasMaxLength(100);

            builder.Property(l => l.Address)
                .HasMaxLength(250);

            builder.Property(l => l.Phone)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.Property(l => l.DepositAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(l => l.LoanDate)
                .IsRequired();

            builder.Property(l => l.Notes)
                .HasColumnType("nvarchar(max)");

            // Relationships
            builder.HasOne(l => l.Device)
                .WithMany(d => d.Loans)
                .HasForeignKey(l => l.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.DepositType)
                .WithMany(dt => dt.Loans)
                .HasForeignKey(l => l.DepositTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(l => l.DeviceId).HasDatabaseName("IX_Loans_DeviceId");
            builder.HasIndex(l => l.LoanDate).HasDatabaseName("IX_Loans_LoanDate");
        }
    }
}
