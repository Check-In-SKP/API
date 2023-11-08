using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CheckInSKP.Infrastructure.Entities;

namespace CheckInSKP.Infrastructure.Data.Configurations
{
    public class StaffEntityConfig : IEntityTypeConfiguration<StaffEntity>
    {
        public void Configure(EntityTypeBuilder<StaffEntity> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.HasOne(staff => staff.User)
                   .WithOne(user => user.Staff)
                   .HasForeignKey<StaffEntity>(staff => staff.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(staff => staff.CardNumber).IsUnique();
            builder.HasIndex(staff => staff.PhoneNumber).IsUnique();
        }
    }
}
