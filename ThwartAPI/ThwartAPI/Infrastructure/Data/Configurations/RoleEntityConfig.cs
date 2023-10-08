using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThwartAPI.Domain.Entities;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data.Configurations
{
    public class RoleEntityConfig : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
