using LogiTrack.Infrastructure;
using LogiTrack.Application;

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

        builder.Services.AddMemoryCache();

        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
