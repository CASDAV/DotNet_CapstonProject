using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Features.InventoryItems;

public class CreateInventoryItem
{
    private readonly IInventoryItemRepository _repository;
    private readonly IMapper _mapper;

    public CreateInventoryItem(IInventoryItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> ExecuteAsync(CreateInventoryItemDTO newItem)
    {
        return await _repository.AddInventoryItemAsync(_mapper.Map<InventoryItem>(newItem));
    }
}
