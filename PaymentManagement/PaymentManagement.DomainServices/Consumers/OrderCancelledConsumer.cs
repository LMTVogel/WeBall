using Events;
using MassTransit;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Consumers;

public class OrderCancelledConsumer(IPaymentService service) : IConsumer<OrderCancelled>
{
    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var orderCancelled = context.Message;
        await service.FailPaymentAsync(orderCancelled.OrderId);
    }
}