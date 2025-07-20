using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogiTrack.Domain.Entities.BusinessObjects;

public class InventoryItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public string Location { get; set; } = null!;

    // Foreign key to associate with an Order
    [ForeignKey("Order")]
    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; } // Navigation property
}
