using CheckInSKP.Application;
using CheckInSKP.Infrastructure;
using CheckInSKP.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration file
        builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: false, reloadOnChange: true);

        // Add services to the container.
        builder.Services.AddLogging();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));

        // Adding services from other projects
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        // Add JWT authentication
        var jwtKey = builder.Configuration["JwtSettings:Key"];

        if (string.IsNullOrWhiteSpace(jwtKey))
        {
            throw new Exception("JWT Key is missing from configuration file.");
        }

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Validate the JWT Issuer (iss) claim
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],

                    // Validate the JWT Audience (aud) claim
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],

                    // Validate the token expiry
                    ValidateLifetime = true,

                    // Validate the presence of the SigningKey
                    ValidateIssuerSigningKey = true,

                    // Specify the SigningKey
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

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
    }
}