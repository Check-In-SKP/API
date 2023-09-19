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
    }
}
