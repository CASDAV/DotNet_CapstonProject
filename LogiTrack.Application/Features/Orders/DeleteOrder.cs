using LogiTrack.Application.Interfaces.BusinessRepositories;

namespace LogiTrack.Application.Features.Orders;

public class DeleteOrder
{
    private readonly IOrdersRepository _ordersRepository;

    private readonly IInventoryItemRepository _inventoryRepository;

    public DeleteOrder(IOrdersRepository ordersRepository, IInventoryItemRepository inventoryRepository)
    {
        _ordersRepository = ordersRepository;
        _inventoryRepository = inventoryRepository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        bool itemsResult = await _inventoryRepository.DeleteInventoryItemsByOrderId(id);

        bool ordersResult = await _ordersRepository.DeleteOrderAsync(id);

        return itemsResult && ordersResult;
    }
}
