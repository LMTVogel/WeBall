using System.Text;
using System.Text.Json;
using NotificationService.Application.Interfaces;
using RabbitMQ.Client;

namespace NotificationService.Infrastructure.Data;

public class RabbitMqProducer(IRabbitMqConnection connection) : IMessageProducer
{
    public void SendMessage<T>(string exchangeName, T message)
    {
        using var channel = connection.Connection.CreateModel();
        channel.QueueDeclare("test", false, false, false, null);
        
        var json = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(json);

        channel.BasicPublish("", "test", null, body);
    }
}