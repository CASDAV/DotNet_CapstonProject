using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.Exceptions;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Features.InventoryItems;

public class GetInventoryItemById
{
    private readonly IInventoryItemRepository _repository;

    private readonly IMapper _mapper;

    public GetInventoryItemById(IInventoryItemRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<DetailedInventoryItemDTO> ExecuteAsync(int id)
    {
        InventoryItem? item = await _repository.GetInventoryItemByIdAsync(id);

        if (item == null) throw new EntityNotFoundException($"Entity not found for {typeof(InventoryItem).FullName} class serching the ID {id}");

        return _mapper.Map<DetailedInventoryItemDTO>(item);
    }
}
