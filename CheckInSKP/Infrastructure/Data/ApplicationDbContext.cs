using CheckInSKP.Domain.Entities;
using CheckInSKP.Domain.Enums;
using CheckInSKP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace CheckInSKP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Commands for 'Package Manager Console' to create and update the database
        // dotnet ef migrations add TestMigration --project Infrastructure --startup-project CheckInAPI --output-dir Data/Migrations
        // dotnet ef database update TestMigration --project Infrastructure --startup-project CheckInAPI
        // dotnet ef migrations remove --project Infrastructure --startup-project CheckInAPI

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TimeLogEntity> TimeLogs { get; set; }
        public DbSet<TimeTypeEntity> TimeTypes { get; set; }
        public DbSet<StaffEntity> Staffs { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seeds roles from enum
            builder.Entity<RoleEntity>().HasData(
                Enum.GetValues(typeof(RoleEnum))
                    .Cast<RoleEnum>()
                    .Select(role => new RoleEntity { Id = (int)role, Name = Role.GetNameFromEnum(role) })
                    .ToArray()
            );

            // Seeds timetypes
            builder.Entity<TimeTypeEntity>().HasData(
                Enum.GetValues(typeof(TimeTypeEnum))
                    .Cast<TimeTypeEnum>()
                    .Select(timeType => new TimeTypeEntity { Id = (int)timeType, Name = TimeType.GetNameFromEnum(timeType) })
                    .ToArray()
            );
        }
    }
}
