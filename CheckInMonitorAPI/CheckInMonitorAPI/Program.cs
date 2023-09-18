using CheckInMonitorAPI;
using CheckInMonitorAPI.Data;
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
