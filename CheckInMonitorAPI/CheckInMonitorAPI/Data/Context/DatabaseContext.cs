using CheckInMonitorAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CheckInMonitorAPI.Data.Context
{
    public class DatabaseContext : DbContext
    {
        // Commands for 'Package Manager Console' to create and update the database
        // dotnet ef migrations remove --project CheckInMonitorAPI
        // dotnet ef migrations add InitialMigration --project CheckInMonitorAPI --output-dir Data/Context/Migrations
        // dotnet ef database update --project CheckInMonitorAPI

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }
        public DbSet<TimeType> TimeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Predetermined data
            modelBuilder.Entity<Role>().HasData(
                    new Role { Id = 1, Name = "Admin" },
                    new Role { Id = 2, Name = "User" },
                    new Role { Id = 3, Name = "Monitor" }
            );

            modelBuilder.Entity<TimeType>().HasData(
                new TimeType { Id = 1, Name = "Check In" },
                new TimeType { Id = 2, Name = "Check Out" }
            );

            #region Constraints
            // Card number is unique for users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.CardNumber)
                .IsUnique();

            // Username is unique for users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            // Phone number is unique for users
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();

            // Role name is unique for roles
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // TimeType name is unique for timetypes
            modelBuilder.Entity<TimeType>()
                .HasIndex(tt => tt.Name)
                .IsUnique();
            #endregion
        }
    }
}
