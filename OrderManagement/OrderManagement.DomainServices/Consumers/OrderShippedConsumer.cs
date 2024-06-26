using Events;
using MassTransit;
using OrderManagement.Domain;
using OrderManagement.DomainServices;

namespace NotificationService.Application.Consumers;

public class OrderShippedConsumer(IOrderService service) : IConsumer<OrderShipped>
{
    public async Task Consume(ConsumeContext<OrderShipped> context)
    {
        var @event = context.Message;
        var order = await service.GetOrderById(@event.OrderId);
        order.OrderStatus = OrderStatus.Shipped;
        await service.UpdateOrderAsync(order.OrderId, order);
    }
}