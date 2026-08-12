using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BeyadAmi.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).ValueGeneratedOnAdd();
            builder.Property(u => u.UserName).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(200);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.PasswordHash).HasMaxLength(500);
            builder.Property(u => u.IsActive).IsRequired();
            builder.Property(u => u.CreatedAt).IsRequired();
            // Role mapping: store as string for clarity and stability
            builder.Property(u => u.Role)
                   .HasMaxLength(20)
                   .HasConversion<string>()
                   .IsRequired()
                   .HasDefaultValue(UserRole.User);
        }
    }
}
