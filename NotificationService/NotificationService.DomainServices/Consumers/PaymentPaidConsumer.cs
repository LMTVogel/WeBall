using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace NotificationService.Application.Consumers;

public class PaymentPaidConsumer(ILogger<PaymentPaidConsumer> logger): IConsumer<PaymentPaid>
{
    public Task Consume(ConsumeContext<PaymentPaid> context)
    {
        var paymentPaid = context.Message;
        logger.LogInformation("Payment paid: {PaymentPaid}", paymentPaid);
        return Task.CompletedTask;
    }
}