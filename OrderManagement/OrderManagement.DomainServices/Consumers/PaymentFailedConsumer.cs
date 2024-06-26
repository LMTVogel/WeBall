using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain;

namespace OrderManagement.DomainServices.Consumers;

public class PaymentFailedConsumer(IOrderService service, ILogger<PaymentFailedConsumer> logger)
    : IConsumer<PaymentFailed>
{
    public async Task Consume(ConsumeContext<PaymentFailed> context)
    {
        logger.LogInformation("Payment was cancelled");
        var @event = context.Message;
        var order = await service.GetOrderById(@event.OrderId);
        order.PaymentStatus = @event.Status;
        await service.UpdateOrderAsync(order.OrderId, order);
    }
}