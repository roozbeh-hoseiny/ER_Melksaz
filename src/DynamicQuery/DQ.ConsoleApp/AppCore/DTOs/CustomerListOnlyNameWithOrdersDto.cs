namespace DQ.ConsoleApp.AppCore.DTOs;

public sealed class CustomerListOnlyNameWithOrdersDto
{
    public string Name { get; set; } = string.Empty;
    public ICollection<OrderOnlyAmnoynDto> Orders { get; set; } = [];
}
