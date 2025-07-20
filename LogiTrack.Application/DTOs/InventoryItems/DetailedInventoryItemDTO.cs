using System;
using LogiTrack.Domain.Entities.BusinessObjects;

namespace LogiTrack.Application.DTOs.InventoryItems;

public class DetailedInventoryItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public string Location { get; set; } = null!;
    public int? OrderId { get; set; }
    public string Summary { get => $"Item No.{Id} | Name: {Name} | Quantity: {Quantity} | Location: {Location} | Order: {(OrderId.HasValue? OrderId.Value: "Not assigned")}"; }

}
