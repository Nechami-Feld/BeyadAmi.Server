using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class DeviceTemplateConfiguration : IEntityTypeConfiguration<DeviceTemplate>
    {
        public void Configure(EntityTypeBuilder<DeviceTemplate> builder)
        {
            builder.ToTable("DeviceTemplates");

            builder.HasKey(dt => dt.TemplateId);

            builder.Property(dt => dt.TemplateName)
                .HasMaxLength(200);

            builder.Property(dt => dt.FilePath)
                .HasMaxLength(500);

            builder.Property(dt => dt.CreatedDate)
                .IsRequired();

            builder.HasOne(dt => dt.DeviceType)
                .WithMany(t => t.DeviceTemplates)
                .HasForeignKey(dt => dt.DeviceTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(dt => dt.DeviceTypeId).HasDatabaseName("IX_DeviceTemplates_DeviceTypeId");
        }
    }
}
