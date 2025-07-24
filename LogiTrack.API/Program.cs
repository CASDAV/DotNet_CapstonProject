using LogiTrack.Infrastructure;
using LogiTrack.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using LogiTrack.API.Services;
using LogiTrack.API.Middleware;

namespace LogiTrack.Main;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Logging.AddConsole().AddDebug();

        // Add services to the container.
        builder.Services.AddInfrastructureSevices(builder.Configuration);   
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers();

        builder.Services.AddSingleton<TokenService>();

        builder.Services.AddMemoryCache();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var jwtSettings = builder.Configuration.GetSection("JwtSettings");
        var jwtKey = builder.Configuration.GetValue<string>("JWT_KEY");
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
                };
            });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseCustomExceptionHandler();
        }

        app.UseHttpsRedirection();

        app.UseRequestTiming();

        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
