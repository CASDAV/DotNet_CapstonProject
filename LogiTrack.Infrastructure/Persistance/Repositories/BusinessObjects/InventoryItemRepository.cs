using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Infrastructure.Persistance.Repositories.BusinsessObjects;

internal class InventoryItemRepository : IInventoryItemRepository
{

    private readonly LogiTrackDBContext _context;

    public InventoryItemRepository(LogiTrackDBContext context)
    {
        _context = context;
    }

    public async Task<int> AddInventoryItemAsync(InventoryItem item)
    {
        await _context.InventoryItems.AddAsync(item);
        await _context.SaveChangesAsync();

        return item.Id;
    }

    public async Task AddInventoryItemCollectionAsync(List<InventoryItem> inventoryItems)
    {
        await _context.InventoryItems.AddRangeAsync(inventoryItems);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteInventoryItemAsync(int id)
    {
        InventoryItem? item = await _context.InventoryItems.FindAsync(id);
        if (item != null)
        {
            _context.InventoryItems.Remove(item);
            return 0 < await _context.SaveChangesAsync();
        }

        return false;
    }

    public async Task<bool> DeleteInventoryItemsByOrderId(int id)
    {

        return -1 != await _context.InventoryItems
            .Where(x => x.OrderId == id)
            .ExecuteDeleteAsync();

    }

    public async Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync()
    {
        return await _context.InventoryItems
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<InventoryItem?> GetInventoryItemByIdAsync(int id)
    {
        return await _context.InventoryItems.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<InventoryItem>> GetInventoryItemsByOrderIdAsync(int orderId)
    {
        return await _context.InventoryItems
            .AsNoTracking()
            .Where(i => i.OrderId == orderId)
            .ToListAsync();
    }

    public async Task UpdateInventoryItemAsync(InventoryItem item)
    {
        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync();
    }
}
