using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderUpdatedConsumer(IEmailNotifier notifier, ILogger<OrderUpdatedConsumer> logger)
    : IConsumer<OrderUpdated>
{
    public Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var order = context.Message;
        logger.LogInformation("Order updated: {Order}", order);
        notifier.SendEmailAsync("laurens.weterings@gmail.com", $"Order Updated {order.OrderId}",
            "Your order has been updated. Please check the order status.");
        return Task.CompletedTask;
    }
}