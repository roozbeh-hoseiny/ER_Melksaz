using SampleConsoleApp.Shared;

namespace SampleConsoleApp.AppCore.Events;

public abstract record EventBase<TId>(TId Id) : IEventMessage
{
    public DateTimeOffset OccuredOn { get; set; } = DateTimeOffset.Now;
    public virtual string BusinessId => this.Id.ToString();
    public Ulid EventId => Ulid.NewUlid();
}
public sealed record OrderCreatedEvent(long Id) : EventBase<long>(Id)
{
    public override string BusinessId => $"order-{this.Id}";
}

public sealed record SendEmailEvent(long Id) : EventBase<long>(Id);
public sealed record SendSmsEvent(long Id) : EventBase<long>(Id);
