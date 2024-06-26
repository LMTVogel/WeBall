using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class OrderCancelledConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<OrderCancelledConsumer> logger)
    : IConsumer<OrderCancelled>
{
    public async Task Consume(ConsumeContext<OrderCancelled> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        var notification = new Notification
        {
            OrderId = order.OrderId,
            Subject = $"Order #{order.OrderId} cancelled",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(order.ClientEmail, notification.Subject, notification.Message);
    }
}