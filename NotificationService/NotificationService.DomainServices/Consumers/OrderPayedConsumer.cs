using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class OrderPayedConsumer(
    ILogger<OrderPayedConsumer> logger,
    IRepository<Notification> repo,
    IEmailNotifier notifier)
    : IConsumer<OrderPayed>
{
    public async Task Consume(ConsumeContext<OrderPayed> context)
    {
        var @event = context.Message;
        logger.LogInformation("Order shipped: {Order}", @event);
        notifier.SendEmailAsync(@event.CustomerEmail, $"Order #{@event.OrderId} payed",
            "Your order has been payed.");

        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Order #{@event.OrderId} payed",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);
    }
}