

using AutoMapper;
using LogiTrack.Application.Interfaces.BusinessRepositories;

namespace LogiTrack.Application.Features.InventoryItems;

public class DeleteInventoryItem
{
    private readonly IInventoryItemRepository _repository;

    private readonly IMapper _mapper;

    public DeleteInventoryItem(IInventoryItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<bool> ExecuteAsync(int id)
    {
        return await _repository.DeleteInventoryItemAsync(id);
    }
}
