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
        var paymentPaid = context.Message;
        logger.LogInformation("Payment paid: {PaymentPaid}", paymentPaid);
        var notification = new Notification
        {
            OrderId = paymentPaid.OrderId,
            Subject = $"Payment for order #{paymentPaid.OrderId} received",
            Message = "Your payment has been received. Thank you for your purchase.",
            Recipient = paymentPaid.ClientEmail,
            SentAt = DateTime.Now
        };
        repo.Add(notification);
        await notifier.SendEmailAsync(paymentPaid.ClientEmail, notification.Subject, notification.Message);
    }
}