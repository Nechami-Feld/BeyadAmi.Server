using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Devices");

            builder.HasKey(d => d.DeviceId);

            builder.Property(d => d.DeviceNumber)
                .HasMaxLength(100);

            builder.Property(d => d.Company)
                .HasMaxLength(100);

            builder.Property(d => d.Notes)
                .HasColumnType("nvarchar(max)");

            builder.Property(d => d.CreatedDate)
                .IsRequired();

            // Relationships
            builder.HasOne(d => d.DeviceType)
                .WithMany(dt => dt.Devices)
                .HasForeignKey(d => d.DeviceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Branch)
                .WithMany(b => b.Devices)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Loans)
                .WithOne(l => l.Device)
                .HasForeignKey(l => l.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Indexes
            builder.HasIndex(d => d.DeviceNumber).HasDatabaseName("IX_Devices_DeviceNumber");
        }
    }
}
