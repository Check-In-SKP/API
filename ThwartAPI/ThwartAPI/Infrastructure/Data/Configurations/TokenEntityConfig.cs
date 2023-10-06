using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data.Configurations
{
    public class TokenEntityConfig : IEntityTypeConfiguration<TokenEntity>
    {
        public void Configure(EntityTypeBuilder<TokenEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.JwtId).IsUnique();
        }
    }
}
