using AutoMapper;
using LogiTrack.Application.DTOs.InventoryItems;
using LogiTrack.Application.DTOs.Orders;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateInventoryItemDTO, InventoryItem>();
        CreateMap<InventoryItem, SimpleInventoryItemDTO>();
        CreateMap<InventoryItem, DetailedInventoryItemDTO>();

        CreateMap<OrderCreateDTO, Order>();
        CreateMap<Order, OrderDetailsDTO>();
        CreateMap<Order, SimpleOrderDTO>().ForMember(d => d.ItemsQuantity, opt => opt.MapFrom(s => s.Items.Count));
    }
}
