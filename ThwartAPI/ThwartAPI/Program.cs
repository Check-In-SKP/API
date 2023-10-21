using Microsoft.EntityFrameworkCore;
using ThwartAPI;
using ThwartAPI.Domain.Factories;
using ThwartAPI.Domain.Interfaces.Repositories;
using ThwartAPI.Infrastructure.Data;
using ThwartAPI.Infrastructure.Mappings;
using ThwartAPI.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Get's appsetting from machine specific json file
builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: false, reloadOnChange: true);
AppSettings settings = builder.Configuration.Get<AppSettings>() ?? throw new ArgumentNullException(nameof(settings));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(Program));

// Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(settings.ConnectionStrings.CheckInDB));

// Factories
builder.Services.AddTransient<DeviceFactory>();
builder.Services.AddTransient<RoleFactory>();
builder.Services.AddTransient<StaffFactory>();
builder.Services.AddTransient<TimeTypeFactory>();
builder.Services.AddTransient<UserFactory>();

// Mappers
builder.Services.AddScoped<DeviceMapper>();
builder.Services.AddScoped<RoleMapper>();
builder.Services.AddScoped<StaffMapper>();
builder.Services.AddScoped<TimeTypeMapper>();
builder.Services.AddScoped<UserMapper>();

// Repositories
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<ITimeTypeRepository, TimeTypeRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
