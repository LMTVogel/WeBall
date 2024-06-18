using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCancelledConsumer(IEmailNotifier notifier, ILogger<OrderCancelledConsumer> logger)
    : IConsumer<OrderCancelled>
{
    public Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} cancelled",
            "Your order has been cancelled.");
        return Task.CompletedTask;
    }
}