namespace NotificationService.Application.Interfaces;

public interface IMessageHandler
{
    void Start(string exchangeName, IMessageHandlerCallback? callback);
    void Stop();
}
