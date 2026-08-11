namespace DQ.ConsoleApp.AppCore.DTOs;

public sealed class CustomerListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<OrderDto> Orders { get; set; } = [];
}
