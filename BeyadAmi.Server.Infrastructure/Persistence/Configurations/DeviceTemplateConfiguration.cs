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

            builder.HasKey(dt => dt.DeviceTemplateId);

            builder.Property(dt => dt.TemplateName)
                .HasMaxLength(200);

            builder.Property(dt => dt.TemplateText)
                .HasMaxLength(2000);

            builder.Property(dt => dt.CreatedDate)
                .IsRequired();

            builder.HasIndex(dt => dt.DeviceCategoryId).HasDatabaseName("IX_DeviceTemplates_DeviceCategoryId");
        }
    }
}
