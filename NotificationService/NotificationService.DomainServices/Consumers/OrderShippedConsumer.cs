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
        var @event = context.Message;
        logger.LogInformation("Order shipped: {Order}", @event);
        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Order #{@event.OrderId} shipped",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);
    }
}