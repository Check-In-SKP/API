using CheckInSKP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckInSKP.Infrastructure.Data.Configurations
{
    public class TimeLogEntityConfig : IEntityTypeConfiguration<TimeLogEntity>
    {
        public void Configure(EntityTypeBuilder<TimeLogEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
        }
    }
}
