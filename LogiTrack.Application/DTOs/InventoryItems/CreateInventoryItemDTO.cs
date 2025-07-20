using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Application.DTOs.InventoryItems;

public class CreateInventoryItemDTO
{
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    public string Location { get; set; } = null!;
    [Required]
    public int Quantity { get; set; }
    public int? OrderId { get; set; }
}
