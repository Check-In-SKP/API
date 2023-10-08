using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;
using ThwartAPI.Domain.Entities;
using ThwartAPI.Infrastructure.Data.Configurations;
using ThwartAPI.Infrastructure.Data.Entities;

namespace ThwartAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Commands for 'Package Manager Console' to create and update the database
        // dotnet ef migrations add InitialMigration --project ThwartAPI --output-dir Infrastructure/Data/Migrations
        // dotnet ef database update --project ThwartAPI
        // dotnet ef migrations remove --project ThwartAPI

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TimeLogEntity> TimeLogs { get; set; }
        public DbSet<TimeTypeEntity> TimeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Apply configurations
            //modelBuilder.ApplyConfiguration(new UserEntityConfig());
            //modelBuilder.ApplyConfiguration(new RoleEntityConfig());
            //modelBuilder.ApplyConfiguration(new TimeLogEntityConfig());
            //modelBuilder.ApplyConfiguration(new TimeTypeEntityConfig());
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data
            builder.Entity<RoleEntity>().HasData(
                    new RoleEntity { Id = 1, Name = "Admin" },
            new RoleEntity { Id = 2, Name = "User" },
                    new RoleEntity { Id = 3, Name = "Monitor" }
            );

            builder.Entity<TimeTypeEntity>().HasData(
                new TimeTypeEntity { Id = 1, Name = "Check In" },
                new TimeTypeEntity { Id = 2, Name = "Check Out" }
            );
        }
    }
}
