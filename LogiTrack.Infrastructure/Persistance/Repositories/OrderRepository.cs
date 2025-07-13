using LogiTrack.Application.Interfaces;
using LogiTrack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LogiTrack.Infrastructure.Persistance.Repositories;

internal class OrderRepository : IOrdersRepository
{
    private readonly LogiTrackDBContext _context;

    public OrderRepository(LogiTrackDBContext context)
    {
        _context = context;
    }

    // Create
    public async Task AddOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    // Read
    public async Task<Order?> GetOrderByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Items) // Include related InventoryItems
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Items) // Include related InventoryItems
            .ToListAsync();
    }

    // Update
    public async Task UpdateOrderAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();
    }

    // Delete
    public async Task DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order != null)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
