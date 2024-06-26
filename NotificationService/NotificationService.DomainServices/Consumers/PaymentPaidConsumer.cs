using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class PaymentPaidConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<PaymentPaidConsumer> logger) : IConsumer<PaymentPaid>
{
    public async Task Consume(ConsumeContext<PaymentPaid> context)
    {
        var @event = context.Message;
        logger.LogInformation("Payment paid: {PaymentPaid}", @event);
        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Payment for order #{@event.OrderId} received",
            Message = "Your payment has been received. Thank you for your purchase.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);
    }
}