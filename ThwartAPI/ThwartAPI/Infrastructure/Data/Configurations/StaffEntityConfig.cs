using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data.Configurations
{
    public class StaffEntityConfig : IEntityTypeConfiguration<StaffEntity>
    {
        public void Configure(EntityTypeBuilder<StaffEntity> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();
            builder.HasIndex(u => u.CardNumber).IsUnique();
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
        }
    }
}
