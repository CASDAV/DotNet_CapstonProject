using LogiTrack.Application.Interfaces;
using LogiTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Infrastructure.Persistance.Repositories;

internal class InventoryItemRepository : IInventoryItemRepository
{

    private readonly LogiTrackDBContext _context;

    public InventoryItemRepository(LogiTrackDBContext context)
    {
        _context = context;
    }

    public async Task AddInventoryItemAsync(InventoryItem item)
    {
        await _context.InventoryItems.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteInventoryItemAsync(int id)
    {
        InventoryItem? item = await _context.InventoryItems.FindAsync();
        if (item != null)
        {
            _context.InventoryItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<InventoryItem>> GetAllInventoryItemsAsync()
    {
        return await _context.InventoryItems
            .ToListAsync();
    }

    public async Task<InventoryItem?> GetInventoryItemByIdAsync(int id)
    {
        return await _context.InventoryItems.FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IEnumerable<InventoryItem>> GetInventoryItemsByOrderIdAsync(int orderId)
    {
        return await _context.InventoryItems.Where(i => i.OrderId == orderId).ToListAsync();
    }

    public async Task UpdateInventoryItemAsync(InventoryItem item)
    {
        _context.InventoryItems.Update(item);
        await _context.SaveChangesAsync();
    }
}
