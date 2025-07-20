using LogiTrack.Application.DTOs.InventoryItems;

namespace LogiTrack.Application.DTOs.Orders;

public class OrderDetailsDTO
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;

    public DateTime DatePlaced { get; set; }

    public List<SimpleInventoryItemDTO>? Items { get; set; }

    public string Summary { get => $"Order # {Id} for {CustomerName} | Items: {(Items != null? Items.Count : 0) } | Placed: {DatePlaced:dd/MM/yyyy}"; }
}
