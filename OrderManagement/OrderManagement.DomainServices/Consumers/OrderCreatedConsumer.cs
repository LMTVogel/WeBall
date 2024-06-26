using MassTransit;
using OrderManagement.Domain;
using Events;

namespace OrderManagement.DomainServices.Consumers;

public class OrderCreatedConsumer(IOrderRepository orderRepo) : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var @event = context.Message;
        var order = new Order();
        order.Apply(@event);
        return orderRepo.CreateOrder(order);
    }
}