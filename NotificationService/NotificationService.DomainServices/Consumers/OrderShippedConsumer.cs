using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;

namespace NotificationService.Application.Consumers;

public class OrderShippedConsumer(ILogger<OrderShippedConsumer> logger, IEmailNotifier notifier)
    : IConsumer<OrderShippedConsumer>
{
    public Task Consume(ConsumeContext<OrderShippedConsumer> context)
    {
        logger.LogInformation("Order shipped: {Order}", context.Message);
        return Task.CompletedTask;
    }
}