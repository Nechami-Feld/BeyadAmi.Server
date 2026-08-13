using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            // Map to table "Company" with columns CompanyId and CompanyName
            builder.ToTable("Company");

            builder.HasKey(c => c.CompanyId);
            builder.Property(c => c.CompanyId)
                   .HasColumnName("CompanyId")
                   .ValueGeneratedOnAdd();

            builder.Property(c => c.CompanyName)
                   .HasColumnName("CompanyName")
                   .HasMaxLength(200)
                   .IsRequired();
        }
    }
}
