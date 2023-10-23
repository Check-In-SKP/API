using CheckInSKP.Domain.Factories;
using CheckInSKP.Domain.Interfaces.Repositories;
using CheckInSKP.Infrastructure.Data;
using CheckInSKP.Infrastructure.Mappings;
using CheckInSKP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckInSKP.Infrastructure
{
    public static class Dependencies
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            // Database
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("CheckInDB")));

            // Unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Factories
            services.AddTransient<DeviceFactory>();
            services.AddTransient<RoleFactory>();
            services.AddTransient<StaffFactory>();
            services.AddTransient<TimeTypeFactory>();
            services.AddTransient<UserFactory>();

            // Mappers
            services.AddScoped<DeviceMapper>();
            services.AddScoped<RoleMapper>();
            services.AddScoped<StaffMapper>();
            services.AddScoped<TimeTypeMapper>();
            services.AddScoped<UserMapper>();

            // Repositories
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStaffRepository, StaffRepository>();
            services.AddScoped<ITimeTypeRepository, TimeTypeRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
