using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Domain.Entities;

public class Order
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string CustomerName { get; set; }

    [Required]
    public DateTime DatePlaced { get; set; } = DateTime.UtcNow;

    // Navigation property for related InventoryItems
    public virtual List<InventoryItem> Items { get; set; } = new();
}
