using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Application.Consumers;

public class PaymentCancelledConsumer(ILogger<PaymentCancelledConsumer> logger): IConsumer<PaymentCancelled>
{
    public Task Consume(ConsumeContext<PaymentCancelled> context)
    {
        var paymentCancelled = context.Message;
        logger.LogInformation("Payment cancelled: {PaymentCancelled}", paymentCancelled);
        return Task.CompletedTask;
    }
}