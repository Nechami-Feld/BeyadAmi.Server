using BeyadAmi.Server.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeyadAmi.Server.Infrastructure.Persistence.Configurations
{
    public class DepositTypeConfiguration : IEntityTypeConfiguration<DepositType>
    {
        public void Configure(EntityTypeBuilder<DepositType> builder)
        {
            builder.HasKey(x => x.DepositTypeId);

            builder.Property(x => x.DepositTypeName)
                .IsRequired();

            builder.HasData(
                new DepositType
                {
                    DepositTypeId = 1,
                    DepositTypeName = "מזומן"
                },
                new DepositType
                {
                    DepositTypeId = 2,
                    DepositTypeName = "צ'ק"
                }
            );
        }
    }
}