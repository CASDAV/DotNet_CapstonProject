namespace LogiTrack.Application.DTOs.InventoryItems;

public class SimpleInventoryItemDTO
{
    public string Name { get; set; } = null!;
    
    public int Quantity { get; set; }

    public string Location { get; set; } = null!;

    public string Summary { get => $"Item: {Name} | Quantity: {Quantity} | Location: {Location}"; }
}
