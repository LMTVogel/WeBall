using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderPayedConsumer(ILogger<OrderPayedConsumer> logger, IEmailNotifier notifier)
    : IConsumer<OrderPayed>
{
    public Task Consume(ConsumeContext<OrderPayed> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} shipped",
            "Your order has been shipped.");
        return Task.CompletedTask;
    }
}