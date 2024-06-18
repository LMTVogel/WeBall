using MassTransit;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;

namespace NotificationService.Application.Consumers;

public class OrderPayedConsumer(ILogger<OrderPayedConsumer> logger, IEmailNotifier notifier)
    : IConsumer<OrderPayedConsumer>
{
    public Task Consume(ConsumeContext<OrderPayedConsumer> context)
    {
        logger.LogInformation("Order payed: {Order}", context.Message);
        return Task.CompletedTask;
    }
}