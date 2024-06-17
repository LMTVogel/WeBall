using RabbitMQ.Client;

namespace NotificationService.Application.Interfaces;

public interface IRabbitMqConnection
{
    IConnection Connection { get; }
}