using LogiTrack.Application.Interfaces.BusinessRepositories;

namespace LogiTrack.Application.Features.InventoryItems;

public class DeleteInventoryItem
{
    private readonly IInventoryItemRepository _repository;

    public DeleteInventoryItem(IInventoryItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        return await _repository.DeleteInventoryItemAsync(id);
    }
}
