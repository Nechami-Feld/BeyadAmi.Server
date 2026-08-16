using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class DeviceCategoryConfiguration : IEntityTypeConfiguration<DeviceCategory>
    {
        public void Configure(EntityTypeBuilder<DeviceCategory> builder)
        {
            builder.ToTable("DeviceCategories");

            builder.HasKey(dc => dc.CategoryId);

            builder.Property(dc => dc.CategoryName)
                .HasMaxLength(200);

            builder.Property(dc => dc.Description)
                .HasMaxLength(1000)
                    .HasColumnType("text");


            builder.HasMany(dc => dc.Devices)
                .WithOne(d => d.Category)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(dc => dc.CategoryName).HasDatabaseName("IX_DeviceCategories_CategoryName");
        }
    }
}
