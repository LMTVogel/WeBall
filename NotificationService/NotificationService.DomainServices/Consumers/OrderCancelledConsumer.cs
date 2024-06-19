using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCancelledConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<OrderCancelledConsumer> logger)
    : IConsumer<OrderCancelled>
{
    public Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} cancelled",
            "Your order has been cancelled.");

        var notification = new Notification
        {
            OrderId = order.OrderId,
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);

        return Task.CompletedTask;
    }
}