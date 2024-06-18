using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(IEmailNotifier notifier, ILogger<OrderCreatedConsumer> logger) : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        logger.LogInformation("Order created: {Order}", context.Message);
        return Task.CompletedTask;
    }
}