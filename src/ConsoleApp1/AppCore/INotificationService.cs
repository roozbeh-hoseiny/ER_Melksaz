namespace ConsoleApp1.AppCore;

internal interface INotificationService
{
    void Send(string message);
}

internal sealed class SmsService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"I am Service : '{this.GetType()}'. sending '{message}'");
}
internal sealed class EmailService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"I am Service : '{this.GetType()}'. sending '{message}'");
}
internal sealed class SlackService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"I am Service : '{this.GetType()}'. sending '{message}'");
}