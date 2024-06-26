using MassTransit;
using OrderManagement.Domain;
using Events;
using Microsoft.Extensions.Logging;

namespace OrderManagement.DomainServices.Consumers;

public class OrderUpdatedConsumer(IOrderRepository orderRepo, ILogger<OrderUpdatedConsumer> logger) : IConsumer<OrderUpdated>
{
    public Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var @event = context.Message;
        var order = new Order();
        order.Apply(@event);
        logger.LogInformation($"Order updated: {order.OrderId}");
        return orderRepo.UpdateOrder(order);
    }
}