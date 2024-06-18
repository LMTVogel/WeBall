using System.Text;
using System.Text.Json;
using NotificationService.Application.Interfaces;
using Polly;
using RabbitMQ.Client;
using Serilog;

namespace NotificationService.Infrastructure.Data;

public class RabbitMqMessageProducer : IMessageProducer, IDisposable
{
    private IModel? _model;
    private IConnection? _connection;

    public RabbitMqMessageProducer()
    {
        Connect();
    }

    private void Connect()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(9, r => TimeSpan.FromSeconds(5), (ex, ts) => { Log.Error("Error connecting to RabbitMQ. Retrying in 5 sec."); })
            .Execute(() =>
            {
                
                var factory = new ConnectionFactory()
                    { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
                factory.AutomaticRecoveryEnabled = true;
                
                _connection = factory.CreateConnection();
                _model = _connection.CreateModel();
                _model.ExchangeDeclare("order.exchange", ExchangeType.Fanout, durable: true, autoDelete: false);
            }); 
    }

    public void Dispose()
    {
        _model?.Dispose();
        _model = null;
        _connection?.Dispose();
        _connection = null;
    }

    public Task PublishMessageAsync(string messageType, object message, string routingKey)
    {
        return Task.Run(() =>
        {
            var data = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(data);
            var properties = _model.CreateBasicProperties(); 
            properties.Headers = new Dictionary<string, object> { { "MessageType", messageType } };
            _model.BasicPublish("order.exchange", routingKey, null, body);
        });
    }
}