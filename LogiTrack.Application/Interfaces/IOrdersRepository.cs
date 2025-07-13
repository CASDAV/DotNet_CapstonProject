using LogiTrack.Domain.Entities;

namespace LogiTrack.Application.Interfaces;

public interface IOrdersRepository
{
    // Create
    Task AddOrderAsync(Order order);

    // Read
    Task<Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    // Update
    Task UpdateOrderAsync(Order order);

    // Delete
    Task DeleteOrderAsync(int id);
}
