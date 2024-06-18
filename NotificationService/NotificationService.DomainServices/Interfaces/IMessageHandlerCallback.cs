namespace NotificationService.Application.Interfaces;

public interface IMessageHandlerCallback
{
    Task<bool> HandleMessageAsync(string messageType, string message);
}