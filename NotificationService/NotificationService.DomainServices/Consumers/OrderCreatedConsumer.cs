using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;

namespace NotificationService.Application.Consumers;

public class OrderCreatedConsumer(IEmailNotifier notifier, ILogger<OrderCreatedConsumer> logger) : IConsumer<OrderCreatedConsumer>
{


    public Task Consume(ConsumeContext<OrderCreatedConsumer> context)
    {
        logger.LogInformation("Order created: {Order}", context.Message);
        return Task.CompletedTask;
    }
}