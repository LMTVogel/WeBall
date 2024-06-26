using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LogisticsManagement.DomainServices.Consumer;

public class PaymentPaidConsumer(
    ILogger<PaymentPaidConsumer> logger, IPublishEndpoint bus) : IConsumer<PaymentPaid>
{
    public async Task Consume(ConsumeContext<PaymentPaid> context)
    {
        var @event = context.Message;
        logger.LogInformation("Payment paid: {PaymentPaid}", @event);
        
        var @shipEvent = new OrderShipped
        {
            OrderId = @event.OrderId,
            CustomerEmail = @event.CustomerEmail
        };
        await bus.Publish(@shipEvent);
    }
}