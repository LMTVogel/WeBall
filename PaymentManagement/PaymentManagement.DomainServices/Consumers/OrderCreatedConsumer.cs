using MassTransit;
using PaymentManagement.Domain.Entities;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Consumers;

public class OrderCreatedConsumer(IPaymentService service) : IConsumer<OrderCreated>
{
    public async Task Consume(ConsumeContext<OrderCreated> context)
    {
        var orderCreated = context.Message;
        var order = new Order()
        {
            Id = orderCreated.OrderId,
            CustomerId = orderCreated.CustomerId,
            Products = orderCreated.Products,
            PaymentMethod = orderCreated.PaymentMethod
        };

        await service.CreatePaymentAsync(order);
    }
}