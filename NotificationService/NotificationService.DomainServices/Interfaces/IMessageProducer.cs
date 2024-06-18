namespace NotificationService.Application.Interfaces;

public interface IMessageProducer
{
    Task PublishMessageAsync(string messageType, object message, string routingKey);
}