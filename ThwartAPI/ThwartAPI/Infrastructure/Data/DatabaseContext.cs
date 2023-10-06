using Microsoft.EntityFrameworkCore;
using System.Data;
using ThwartAPI.Infrastructure.Data.Configurations;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data
{
    public class DatabaseContext : DbContext
    {
        // Commands for 'Package Manager Console' to create and update the database
        // dotnet ef migrations add InitialMigration --project ThwartAPI --output-dir Infrastructure/Data/Migrations
        // dotnet ef database update --project ThwartAPI
        // dotnet ef migrations remove --project ThwartAPI

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TimeLogEntity> TimeLogs { get; set; }
        public DbSet<TimeTypeEntity> TimeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations
            modelBuilder.ApplyConfiguration(new UserEntityConfig());
            modelBuilder.ApplyConfiguration(new RoleEntityConfig());
            modelBuilder.ApplyConfiguration(new TimeLogEntityConfig());
            modelBuilder.ApplyConfiguration(new TimeTypeEntityConfig());

            // Seed data
            modelBuilder.Entity<RoleEntity>().HasData(
                    new RoleEntity { Id = 1, Name = "Admin" },
            new RoleEntity { Id = 2, Name = "User" },
                    new RoleEntity { Id = 3, Name = "Monitor" }
            );

            modelBuilder.Entity<TimeTypeEntity>().HasData(
                new TimeTypeEntity { Id = 1, Name = "Check In" },
                new TimeTypeEntity { Id = 2, Name = "Check Out" }
            );
        }
    }
}
