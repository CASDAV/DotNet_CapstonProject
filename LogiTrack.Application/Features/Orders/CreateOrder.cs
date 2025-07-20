using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Features.Orders;

public class CreateOrder
{
    private readonly IOrdersRepository _repository;
    private readonly IMapper _mapper;
    public CreateOrder(IOrdersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> ExecuteAsync(OrderCreateDTO newOrder)
    {
        return await _repository.AddOrderAsync(_mapper.Map<Order>(newOrder));
    }
}
