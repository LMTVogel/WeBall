namespace NotificationService.Application.Interfaces;

public interface IMessageProducer
{
   void SendMessage<T>(string exchangeName, T message);
}