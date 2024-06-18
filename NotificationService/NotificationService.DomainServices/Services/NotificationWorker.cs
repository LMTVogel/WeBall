using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using Serilog;
using ILogger = Serilog.ILogger;

namespace NotificationService.Application.Services;

public class NotificationWorker(IMessageHandler messageHandler, IEmailNotifier emailNotifier, ILogger<NotificationWorker> logger)
    : IHostedService, IMessageHandlerCallback
{
    
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Notification Worker started");
        messageHandler.Start( this);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Notification Worker stopped");
        messageHandler.Stop();
        return Task.CompletedTask;
    }

    public async Task<bool> HandleMessageAsync(string messageType, string message)
    {
        logger.LogInformation("Message received");
        await emailNotifier.SendEmailAsync("test@gmail.com", "test@gmail.com", "Order created", message);
        return true;
    }
}