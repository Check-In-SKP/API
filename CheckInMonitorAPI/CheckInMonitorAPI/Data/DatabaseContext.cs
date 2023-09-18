using Microsoft.EntityFrameworkCore;

namespace CheckInMonitorAPI.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }


    }
}
