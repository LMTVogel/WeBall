using Events;
using MassTransit;
using PaymentManagement.Domain.Entities;
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
            CustomerName = orderCreated.CustomerName,
            CustomerEmail = orderCreated.CustomerEmail,
            OrderDate = orderCreated.OrderDate,
            Products = orderCreated.Products,
            PriceTotal = orderCreated.PriceTotal,
            OrderStatus = orderCreated.OrderStatus,
            PaymentStatus = orderCreated.PaymentStatus,
            ShippingCompany = orderCreated.ShippingCompany,
            ShippingAddress = orderCreated.ShippingAddress,
            EstimatedDeliveryDate = orderCreated.EstimatedDeliveryDate,
            CreatedAt = orderCreated.CreatedAt,
            UpdatedAt = orderCreated.UpdatedAt
        };

        await service.CreatePaymentAsync(order);
    }
}