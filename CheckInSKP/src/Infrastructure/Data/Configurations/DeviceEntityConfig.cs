using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CheckInSKP.Infrastructure.Entities;

namespace CheckInSKP.Infrastructure.Data.Configurations
{
    public class DeviceEntityConfig : IEntityTypeConfiguration<DeviceEntity>
    {
        public void Configure(EntityTypeBuilder<DeviceEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
        }
    }
}
