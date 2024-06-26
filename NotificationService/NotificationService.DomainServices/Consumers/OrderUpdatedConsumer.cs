using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class OrderUpdatedConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<OrderUpdatedConsumer> logger)
    : IConsumer<OrderUpdated>
{
    public async Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var @event = context.Message;
        logger.LogInformation("Order updated: {Order}", @event);

        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Order #{@event.OrderId} updated",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);
    }
}