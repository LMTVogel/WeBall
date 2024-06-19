using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<OrderCreatedConsumer> logger)
    : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var order = context.Message;
        logger.LogInformation("Order shipped: {Order}", order);
        notifier.SendEmailAsync(order.ClientEmail, $"Order #{order.OrderId} created",
            "Your order has been created.");
        
        var notification = new Notification
        {
            OrderId = order.OrderId,
            Subject = $"Order #{order.OrderId} created",
            Message = "Your order has been updated. Please check the order status.",
            Recipient = order.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);


        return Task.CompletedTask;
    }
}