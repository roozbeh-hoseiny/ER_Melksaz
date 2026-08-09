namespace DQ.ConsoleApp.AppCore.DTOs;

public sealed class OrderDto
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
