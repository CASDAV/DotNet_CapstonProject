using LogiTrack.Domain.Entities;

namespace LogiTrack.Application.Interfaces;

public interface IInventoryItemRepository
{
    // Create
    Task AddInventoryItemAsync(InventoryItem item);

    // Read
    Task<InventoryItem?> GetInventoryItemByIdAsync(int id);
    Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync();
    Task<IEnumerable<InventoryItem>> GetInventoryItemsByOrderIdAsync(int orderId);

    // Update
    Task UpdateInventoryItemAsync(InventoryItem item);

    // Delete
    Task DeleteInventoryItemAsync(int id);
}
