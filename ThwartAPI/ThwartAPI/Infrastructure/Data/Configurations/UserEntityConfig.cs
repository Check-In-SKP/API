using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThwartAPI.Domain.Entities;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data.Configurations
{
    public class UserEntityConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.CardNumber).IsUnique();
            builder.HasIndex(u => u.Username).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
        }
    }
}
