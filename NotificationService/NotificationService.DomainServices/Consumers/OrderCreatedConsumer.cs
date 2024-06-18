using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(IEmailNotifier notifier, ILogger<OrderCreatedConsumer> logger)
    : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} created",
            "Your order has been created.");
        return Task.CompletedTask;
    }
}