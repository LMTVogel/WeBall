using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using Serilog;

namespace NotificationService.Application.Services;

public class NotificationWorker(IMessageHandler messageHandler, IEmailNotifier emailNotifier)
    : IHostedService, IMessageHandlerCallback
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        messageHandler.Start("OrderService", this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        await emailNotifier.SendEmailAsync("test@gmail.com", "test@gmail.com", "Order created", message);
        return true;
    }
}