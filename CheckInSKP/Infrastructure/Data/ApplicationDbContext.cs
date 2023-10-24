using CheckInSKP.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace CheckInSKP.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Commands for 'Package Manager Console' to create and update the database
        // dotnet ef migrations add InitialMigration --project CheckInSKP.Infrastructure --output-dir Infrastructure/Data/Migrations
        // dotnet ef database update --project CheckInSKP.Infrastructure
        // dotnet ef migrations remove --project CheckInSKP.Infrastructure

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TimeLogEntity> TimeLogs { get; set; }
        public DbSet<TimeTypeEntity> TimeTypes { get; set; }
        public DbSet<StaffEntity> Staffs { get; set; }
        public DbSet<TokenEntity> Tokens { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data
            builder.Entity<RoleEntity>().HasData(
                new RoleEntity { Id = 1, Name = "Admin" },
                new RoleEntity { Id = 2, Name = "Moderator" },
                new RoleEntity { Id = 3, Name = "Staff" },
                new RoleEntity { Id = 4, Name = "User" },
                new RoleEntity { Id = 5, Name = "Monitor" }
            );

            builder.Entity<TimeTypeEntity>().HasData(
                new TimeTypeEntity { Id = 1, Name = "Check In" },
                new TimeTypeEntity { Id = 2, Name = "Check Out" }
            );
        }
    }
}
