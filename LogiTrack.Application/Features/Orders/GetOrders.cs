using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Features.Orders;

public class GetOrders
{
    private readonly IOrdersRepository _ordersRepository;
    private readonly IMapper _mapper;

    public GetOrders(IOrdersRepository ordersRepository, IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SimpleOrderDTO>> ExecuteAsync()
    {
         

        return _mapper.Map<List<SimpleOrderDTO>>(await _ordersRepository.GetAllOrdersAsync());

    }
}
