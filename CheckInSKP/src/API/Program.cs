using CheckInSKP.Application;
using CheckInSKP.Infrastructure;
using CheckInSKP.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add configuration file
        builder.Configuration.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: false, reloadOnChange: true);

        //Console.WriteLine(KeyGenerator.GenerateRandomKey(32));

        // Add services to the container.
        builder.Services.AddLogging();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAutoMapper(typeof(Program));

        // Add health checks
        builder.Services.AddHealthChecks();
        builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();

        // Adding services from other projects
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        #region JWT Authentication
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
        #endregion

        #region CORS Policies
        // CORS
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy(name: "Web",
        //                      builder =>
        //                      {
        //                          builder.WithOrigins("http://localhost", "https://localhost")
        //                                 .AllowAnyHeader()
        //                                 .AllowAnyMethod();
        //                          // builder.WithMethods("GET", "POST");
        //                          // builder.AllowCredentials();
        //                      });
        //});

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("localhost",
                builder =>
                {
                    builder.SetIsOriginAllowed(origin =>
                    {
                        return new Uri(origin).Host == "localhost"; // This will allow any port on localhost
                    })
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
        #endregion

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseCors("Web");

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseHealthChecks("/health");
        app.MapControllers();

        app.Run();
    }
}