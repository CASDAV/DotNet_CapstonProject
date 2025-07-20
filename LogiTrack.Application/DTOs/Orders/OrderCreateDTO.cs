using System;
using System.ComponentModel.DataAnnotations;

namespace LogiTrack.Application.DTOs.Orders;

public class OrderCreateDTO
{
    [Required]
    public string CustomerName { get; set; } = null!;

    [Required]
    public DateTime DatePlaced { get; set; }

}
