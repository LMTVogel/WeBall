using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderUpdatedConsumer(IEmailNotifier notifier, ILogger<OrderUpdatedConsumer> logger): IConsumer<OrderUpdated>
{
    public Task Consume(ConsumeContext<OrderUpdated> context)
    {
        logger.LogInformation("Order updated: {Order}", context.Message);
        return Task.CompletedTask;
    }
}