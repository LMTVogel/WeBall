using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderShippedConsumer(ILogger<OrderShippedConsumer> logger, IEmailNotifier notifier)
    : IConsumer<OrderShipped>
{
    public Task Consume(ConsumeContext<OrderShipped> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} shipped",
            "Your order has been shipped.");
        return Task.CompletedTask;
    }
}