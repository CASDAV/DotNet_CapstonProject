using LogiTrack.Application.Features.InventoryItems;
using LogiTrack.Application.Features.Orders;
using LogiTrack.Application.Mapper;
using Microsoft.Extensions.DependencyInjection;

namespace LogiTrack.Application;

public static class ApplicationRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

        //Use cases Services instantiation
        services.AddScoped<CreateOrder>();
        services.AddScoped<DeleteOrder>();
        services.AddScoped<GetOrderById>();
        services.AddScoped<GetOrders>();

        services.AddScoped<CreateInventoryItem>();
        services.AddScoped<DeleteInventoryItem>();
        services.AddScoped<GetInventoryItems>();
        services.AddScoped<GetInventoryItemById>();
        services.AddScoped<GetInventoryItemsByOrderId>();


        return services;
    }
}
