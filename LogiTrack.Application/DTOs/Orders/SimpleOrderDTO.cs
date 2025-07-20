namespace LogiTrack.Application.DTOs.Orders;

public class SimpleOrderDTO
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;

    public DateTime DatePlaced { get; set; }

    public int ItemsQuantity { get; set; }

    public string Summary { get => $"Order # {Id} for {CustomerName} | Items: {ItemsQuantity} | Placed: {DatePlaced:dd/MM/yyyy}"; }
}
