using AutoMapper;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Application.Interfaces.BusinessRepositories;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Features.Orders;

public class GetOrderById
{
    private readonly IOrdersRepository _repository;
    private readonly IMapper _mapper;

    public GetOrderById(IOrdersRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<OrderDetailsDTO?> ExecuteAsync(int id)
    {
        Order? order = await _repository.GetOrderByIdAsync(id);

        if (order == null) return null;

        OrderDetailsDTO orderDetails = _mapper.Map<OrderDetailsDTO>(order);

        return orderDetails;
    }
}
