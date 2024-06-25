using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class OrderShippedConsumer(
    ILogger<OrderShippedConsumer> logger,
    IRepository<Notification> repo,
    IEmailNotifier notifier)
    : IConsumer<OrderShipped>
{
    public async Task Consume(ConsumeContext<OrderShipped> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        var notification = new Notification
        {
            OrderId = order.OrderId,
            Subject = $"Order #{order.OrderId} shipped",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(order.ClientEmail, notification.Subject, notification.Message);
    }
}