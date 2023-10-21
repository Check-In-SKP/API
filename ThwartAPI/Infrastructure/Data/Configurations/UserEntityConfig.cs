using CheckInSKP.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckInSKP.Infrastructure.Data.Configurations
{
    public class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
        }
    }
}
