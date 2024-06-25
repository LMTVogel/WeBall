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
    public Task Consume(ConsumeContext<OrderPayed> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} payed",
            "Your order has been payed.");

        var notification = new Notification
        {
            OrderId = order.OrderId,
            Subject = $"Order #{order.OrderId} payed",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);

        return Task.CompletedTask;
    }
}