using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Interfaces.BusinessRepositories;

namespace LogiTrack.Application.Features.InventoryItems;

public class GetInventoryItemsByOrderId
{
    private readonly IInventoryItemRepository _repository;
    private readonly IMapper _mapper;

    public GetInventoryItemsByOrderId(IInventoryItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SimpleInventoryItemDTO>> ExecuteAsync(int id)
    {
        return _mapper.Map<List<SimpleInventoryItemDTO>>(await _repository.GetInventoryItemsByOrderIdAsync(id));
    }
}
