using Events;
using MassTransit;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Consumers;

public class PaymentCreatedConsumer(IPaymentService service) : IConsumer<PaymentCreated>
{
    public async Task Consume(ConsumeContext<PaymentCreated> context)
    {
        var paymentCreated = context.Message;
        await service.ProcessPaymentAsync(paymentCreated.PaymentId);
    }
}