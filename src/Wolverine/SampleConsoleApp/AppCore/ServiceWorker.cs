using Marten;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SampleConsoleApp.AppCore.Events;
using SampleConsoleApp.Shared;
using System.IO.Hashing;
using System.Text;
using Wolverine;

namespace SampleConsoleApp.AppCore;

internal sealed class ServiceWorker : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IHostApplicationLifetime _lifetime;
    private readonly ILogger<ServiceWorker> _logger;

    public ServiceWorker(
        IMessageBus bus,
        IHostApplicationLifetime lifetime,
        ILogger<ServiceWorker> logger)
    {
        this._bus = bus;
        this._lifetime = lifetime;
        this._logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var started = new TaskCompletionSource();

        this._lifetime.ApplicationStarted.Register(() =>
        {
            started.SetResult();
        });

        await started.Task;
        Console.Clear();
        this._logger.LogInformation("Up and run!");
        while (!stoppingToken.IsCancellationRequested)
        {
            await this._bus.PublishAsync(new OrderCreatedEvent(DateTime.Now.Ticks));

            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}

public static class InboxIdGenerator
{
    public static string Create(IEventMessage message, string receiver)
    {
        var raw = $"{message.BusinessId}:{message.GetType().FullName}:{receiver}";

        var bytes = Encoding.UTF8.GetBytes(raw);

        var hash = XxHash128.Hash(bytes);

        return Convert.ToHexString(hash);
    }
}
public sealed class IdempotencyMiddleware
{
    public static async Task<HandlerContinuation> Before(
        IEventMessage message,
        Envelope envelope,
        IDocumentSession session,
        ILogger<IdempotencyMiddleware> logger,
        CancellationToken ct)
    {
        var receiver = envelope.Destination?.ToString() ?? "unknown";
        var doc = new InboxIdempotency
        {
            Id = InboxIdGenerator.Create(message, receiver),
            MessageId = message.BusinessId,
            EventId = message.EventId.ToString(),
            MessageType = message.GetType().FullName!,
            Receiver = receiver,
            LeaseTime = DateTimeOffset.UtcNow.AddDays(30),
            CreatedAt = DateTimeOffset.UtcNow
        };
        session.Insert(doc);
        try
        {
            await session.SaveChangesAsync(ct).ConfigureAwait(false);
            return HandlerContinuation.Continue;
        }
        catch (Exception)
        {
            return HandlerContinuation.Stop;
        }
    }

    public static void After(
        IEventMessage message,
        ILogger<IdempotencyMiddleware> logger,
        CancellationToken ct)
    {
        logger.LogInformation("I AM IdempotencyMiddleware after");
    }
}
public sealed class InboxIdempotency
{
    public required string Id { get; init; }
    public required string MessageId { get; init; }
    public required string EventId { get; init; }
    public required string MessageType { get; init; }
    public required string Receiver { get; init; }
    public DateTimeOffset? LeaseTime { get; init; }
    public DateTimeOffset CreatedAt { get; init; }

    public InboxIdempotency()
    {
        this.CreatedAt = DateTimeOffset.UtcNow;
    }
}

/*
 CREATE TABLE inbox_idempotency
(
    message_id    text        NOT NULL,
    receiver      text        NOT NULL,
    event_id      text        NOT NULL,
    message_type  text        NOT NULL,
    lease_time    timestamptz NULL,
    created_at    timestamptz NOT NULL DEFAULT now(),

    CONSTRAINT pk_inbox_idempotency
        PRIMARY KEY (message_id, receiver)
);

CREATE INDEX ix_inbox_idempotency_event_id
    ON inbox_idempotency(event_id);

CREATE INDEX ix_inbox_idempotency_lease_time
    ON inbox_idempotency(lease_time);
 */