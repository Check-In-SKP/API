using CheckInMonitorAPI;
using CheckInMonitorAPI.Data.Context;
using CheckInMonitorAPI.Data.Repositories.Implementations;
using CheckInMonitorAPI.Data.Repositories.Interfaces;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Implementations;
using CheckInMonitorAPI.Data.Repositories.UnitOfWork.Interfaces;
using CheckInMonitorAPI.Models.Entities;
using CheckInMonitorAPI.Services.Implementations;
using CheckInMonitorAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Get's appsetting from machine specific json file
builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: false, reloadOnChange: true);
AppSettings settings = builder.Configuration.Get<AppSettings>();

// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(settings.ConnectionStrings.CheckInDB));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IGenericRepository<Role, int>, GenericRepository<Role, int>>();
builder.Services.AddScoped<IGenericRepository<TimeLog, int>, GenericRepository<TimeLog, int>>();
builder.Services.AddScoped<IGenericRepository<TimeType, int>, GenericRepository<TimeType, int>>();
builder.Services.AddScoped<IGenericRepository<User, int>, GenericRepository<User, int>>();

builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ITimeLogService, TimeLogService>();
builder.Services.AddScoped<ITimeTypeService, TimeTypeService>();
builder.Services.AddScoped<IUserService, UserService>();

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
