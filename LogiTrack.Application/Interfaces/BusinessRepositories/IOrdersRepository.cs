using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Interfaces.BusinessRepositories;

public interface IOrdersRepository
{
    // Create
    Task<int> AddOrderAsync(Order order);

    // Read
    Task<Order?> GetOrderByIdAsync(int id);
    Task<IEnumerable<Order>> GetAllOrdersAsync();

    // Update
    Task UpdateOrderAsync(Order order);

    // Delete
    Task<bool> DeleteOrderAsync(int id);
}
