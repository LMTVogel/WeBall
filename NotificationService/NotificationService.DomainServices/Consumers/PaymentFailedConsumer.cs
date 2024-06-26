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
        var paymentCancelled = context.Message;
        logger.LogInformation("Payment cancelled: {PaymentCancelled}", paymentCancelled);
        var notification = new Notification
        {
            OrderId = paymentCancelled.OrderId,
            Subject = $"Payment for order #{paymentCancelled.OrderId} cancelled",
            Message = "Your payment has been cancelled. Please contact us for more information.",
            Recipient = paymentCancelled.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(paymentCancelled.ClientEmail, notification.Subject, notification.Message);
    }
}