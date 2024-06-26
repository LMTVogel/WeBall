using Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Consumers;

public class PaymentFailedConsumer(
    IEmailNotifier notifier,
    IRepository<Notification> repo,
    ILogger<PaymentFailedConsumer> logger): IConsumer<PaymentFailed>
{
    public async Task Consume(ConsumeContext<PaymentFailed> context)
    {
        var @event = context.Message;
        logger.LogInformation("Payment cancelled: {PaymentCancelled}", @event);
        var notification = new Notification
        {
            OrderId = @event.OrderId,
            Subject = $"Payment for order #{@event.OrderId} cancelled",
            Message = "Your payment has been cancelled. Please contact us for more information.",
            Recipient = @event.CustomerEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(@event.CustomerEmail, notification.Subject, notification.Message);
    }
}