namespace DQ.ConsoleApp.AppCore.DTOs;

public sealed class CustomerDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<OrderDto> Orders { get; set; } = [];
}
