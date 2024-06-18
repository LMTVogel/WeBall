using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCancelledConsumer(IEmailNotifier notifier, ILogger<OrderCancelledConsumer> logger) : IConsumer<OrderCancelled>
{
    public Task Consume(ConsumeContext<OrderCancelled> context)
    {
        logger.LogInformation("Order cancelled: {Order}", context.Message);
        return Task.CompletedTask;
    }
}
