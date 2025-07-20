using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Interfaces.BusinessRepositories;

public interface IInventoryItemRepository
{
    // Create
    Task<int> AddInventoryItemAsync(InventoryItem item);

    Task AddInventoryItemCollectionAsync(List<InventoryItem> inventoryItems);

    // Read
    Task<InventoryItem?> GetInventoryItemByIdAsync(int id);
    Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync();
    Task<IEnumerable<InventoryItem>> GetInventoryItemsByOrderIdAsync(int orderId);

    // Update
    Task UpdateInventoryItemAsync(InventoryItem item);

    // Delete
    Task<bool> DeleteInventoryItemAsync(int id);

    Task<bool> DeleteInventoryItemsByOrderId(int id);
}
