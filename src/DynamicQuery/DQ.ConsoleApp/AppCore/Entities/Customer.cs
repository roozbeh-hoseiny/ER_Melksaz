namespace DQ.ConsoleApp.AppCore.Entities;

public sealed class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsActive { get; set; }
    public ICollection<Order> Orders { get; set; } = [];
}
