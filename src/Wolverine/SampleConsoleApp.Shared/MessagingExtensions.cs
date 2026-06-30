using JasperFx.Core;
using System.Reflection;
using Wolverine;
using Wolverine.ErrorHandling;
using Wolverine.RabbitMQ;

namespace SampleConsoleApp.Shared;

public static class MessagingExtensions
{
    public static void ConfigureMessaging(
        this WolverineOptions opts,
        Assembly assembly,
        string serviceName)
    {
        string exchangeName = $"{serviceName}-jobs";

        opts.UseRabbitMq("amqp://guest:guest@localhost:5672/")
            .AutoProvision();

        // ONE durable queue per service
        opts.ListenToRabbitQueue(serviceName, queue =>
        {
            queue.IsDurable = true;
            queue.AutoDelete = false;
            //queue.TimeToLive(TimeSpan.FromSeconds(10));

            queue.BindExchange(exchangeName);
        }).UseDurableInbox();

        // Convention for ALL events
        opts.Policies.AutoApplyTransactions();

        opts.Policies.OnException<Exception>()
            .RetryWithCooldown(
                1.Seconds(),
                5.Seconds(),
                15.Seconds());

        opts.Policies.OnException<Exception>()
            .MoveToErrorQueue();

        SetEventsRoutes(opts, exchangeName, assembly);
    }

    private static void SetEventsRoutes(
        WolverineOptions opts,
        string exchangeName,
        Assembly assembly)
    {

        var eventTypes = assembly
            .GetTypes()
            .Where(t =>
                typeof(IEventMessage).IsAssignableFrom(t) &&
                t is
                {
                    IsClass: true,
                    IsAbstract: false,
                    IsInterface: false
                });

        foreach (var eventType in eventTypes)
        {
            opts.PublishMessage(eventType).ToRabbitExchange(exchangeName);
        }
    }
}


public interface IEventMessage
{
    string BusinessId { get; }
    Ulid EventId { get; }
}