using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<OrderCreatedConsumer> logger)
    : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var @event = context.Message;
        logger.LogInformation("Order shipped: {Order}", @event);
        
        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Order #{@event.OrderId} created",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);

        return Task.CompletedTask;
    }
}