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
        var order = context.Message;
        logger.LogInformation("Order updated: {Order}", order);

        var notification = new Notification
        {
            OrderId = order.OrderId,
            Subject = $"Order #{order.OrderId} updated",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(order.ClientEmail, notification.Subject, notification.Message);
    }
}