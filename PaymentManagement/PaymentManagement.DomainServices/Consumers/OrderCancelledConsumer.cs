using MassTransit;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Consumers;

public abstract class OrderCancelledConsumer(IPaymentService service) : IConsumer<OrderCancelled>
{
    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var orderCancelled = context.Message;
        await service.CancelPaymentAsync(orderCancelled.OrderId);
    }
}