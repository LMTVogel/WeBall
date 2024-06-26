using Events;
using MassTransit;
using PaymentManagement.Domain.Entities;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Consumers;

public class OrderUpdatedConsumer(IPaymentService service) : IConsumer<OrderUpdated>
{
    public async Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var @event = context.Message;
        if (@event.OrderStatus == OrderStatus.Cancelled)
        {
            await service.FailPaymentAsync(@event.OrderId);
        }
    }
}