using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;

namespace LogiTrack.Application.Features.InventoryItems;

public class GetInventoryItems
{
    private readonly IInventoryItemRepository _repository;

    private readonly IMapper _mapper;

    public GetInventoryItems(IInventoryItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SimpleInventoryItemDTO>> ExecuteAsync()
    {
        return _mapper.Map<List<SimpleInventoryItemDTO>>(await _repository.GetAllInventoryItemsAsync());
    }
}
