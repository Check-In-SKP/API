using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data.Configurations
{
    public class TimeLogEntityConfig : IEntityTypeConfiguration<TimeLogEntity>
    {
        public void Configure(EntityTypeBuilder<TimeLogEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
        }
    }
}
