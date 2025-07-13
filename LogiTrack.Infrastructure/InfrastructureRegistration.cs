using LogiTrack.Application.Interfaces;
using LogiTrack.Infrastructure.Persistance;
using LogiTrack.Infrastructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LogiTrack.Infrastructure;
/// <summary>
/// Provides extension methods for registering infrastructure services, such as database context and repositories,
/// into the application's dependency injection container.
/// </summary>
public static class InfrastructureRegistration
{
    /// <summary>
    /// Configures and registers the infrastructure-layer services for the application,
    /// including the Entity Framework Core DbContext and repository implementations.
    /// </summary>
    /// <param name="services">The IServiceCollection to which services will be added.</param>
    /// <param name="configuration">The application configuration, used to retrieve connection strings.</param>
    /// <returns>The updated IServiceCollection with infrastructure services registered.</returns>
    /// <remarks>
    /// This method sets up:
    /// - LogiTrackDBContext using MariaDB 10.11.11
    /// - Scoped dependencies for IInventoryItemRepository and IOrdersRepository
    /// </remarks>

    public static IServiceCollection AddInfrastructureSevices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LogiTrackDBContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")))
        );

        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        services.AddScoped<IOrdersRepository, OrderRepository>();

        return services;
    }

}
