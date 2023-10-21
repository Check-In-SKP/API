using Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class TimeTypeEntityConfig : IEntityTypeConfiguration<TimeTypeEntity>
    {
        public void Configure(EntityTypeBuilder<TimeTypeEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.Name).IsUnique();
        }
    }
}
