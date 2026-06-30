using Microsoft.Extensions.Logging;
using SampleConsoleApp.AppCore.Events;
using Wolverine;

namespace SampleConsoleApp.AppCore;

public sealed class OrderCreatedEventHandler
{
    public static async Task Handle(
         OrderCreatedEvent msg,
         Envelope envelope,
         IMessageBus bus,
         ILogger<OrderCreatedEventHandler> logger)
    {
        logger.LogInformation("Handling '{message_type}' by '{handler_type}'. attempt => {attempt}",
            msg.GetType(),
            nameof(OrderCreatedEventHandler),
            envelope.Attempts);


        await bus.PublishAsync(new SendEmailEvent(msg.Id));
        await bus.PublishAsync(new SendSmsEvent(msg.Id));
    }
}
public sealed class SendEmailHandler
{
    public static void Handle(
         SendEmailEvent msg,
         Envelope envelope,
         ILogger<OrderCreatedEventHandler> logger)
    {
        logger.LogInformation("Sending email of order:'{OrderId}'. attempt => {attempt}",
            msg.BusinessId,
            envelope.Attempts);
    }
}
public sealed class SendSmsHandler
{
    public static void Handle(
         SendSmsEvent msg,
         Envelope envelope,
         ILogger<OrderCreatedEventHandler> logger)
    {
        var attempt = envelope.Attempts;
        if (attempt == 1)
            logger.LogInformation("Sending sms of order:'{OrderId}'. attempt => {attempt}",
                msg.BusinessId,
               envelope.Attempts);
        else
            logger.LogWarning("************* Sending sms of order:'{OrderId}'. attempt => {attempt}",
                msg.BusinessId,
               envelope.Attempts);
    }
}
