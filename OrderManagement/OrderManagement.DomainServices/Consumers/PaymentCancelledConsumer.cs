using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain;

namespace OrderManagement.DomainServices.Consumers;

public class PaymentCancelledConsumer(IOrderService service, ILogger<PaymentCancelledConsumer> logger)
    : IConsumer<PaymentCancelled>
{
    public async Task Consume(ConsumeContext<PaymentCancelled> context)
    {
        logger.LogInformation("Payment was cancelled");
        var @event = context.Message;
        var order = await service.GetOrderById(@event.OrderId);
        order.PaymentStatus = PaymentStatus.Failed;
        await service.UpdateOrderAsync(order.OrderId, order);
    }
}