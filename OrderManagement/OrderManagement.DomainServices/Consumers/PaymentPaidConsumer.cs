using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using OrderManagement.Domain;

namespace OrderManagement.DomainServices.Consumers;

public class PaymentPaidConsumer(IOrderService service, ILogger<PaymentPaidConsumer> logger)
    : IConsumer<PaymentPaid>
{
    public async Task Consume(ConsumeContext<PaymentPaid> context)
    {
        logger.LogInformation("Order was paid");
        var @event = context.Message;
        var order = await service.GetOrderById(@event.OrderId);
        order.PaymentStatus = @event.Status;
        await service.UpdateOrderAsync(order.OrderId, order);
    }
}