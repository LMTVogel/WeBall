using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NotificationService.Application.Interfaces;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Infrastructure.Data;

public class RabbitMqMessageHandler : IMessageHandler
{
    private IMessageHandlerCallback? _callback;
    private IModel _model;
    private IConnection _connection;
    private AsyncEventingBasicConsumer? _consumer;
    private readonly ILogger<IMessageHandler> _logger;
    private string _consumerTag;

    public RabbitMqMessageHandler(ILogger<IMessageHandler> logger)
    {
        _logger = logger;
        _logger.LogInformation("Message received");
    }

    public void Start(IMessageHandlerCallback callback)
    {
        _callback = callback;

        Policy.Handle<Exception>()
            .WaitAndRetry(9, r => TimeSpan.FromSeconds(5),
                (ex, ts) => { Console.Error.WriteLine("Error connecting to rabbit mq retrying in 5sec"); })
            .Execute(() =>
            {
                var factory = new ConnectionFactory()
                    { HostName = "localhost", Port = 5672, UserName = "guest", Password = "guest" };
                _connection = factory.CreateConnection();
                _model = _connection.CreateModel();
                _model.ExchangeDeclare("order.exchange", ExchangeType.Fanout, false, false, null);
                _model.QueueDeclare("order.queue", false, false, false, null);
                _model.QueueBind("order.queue", "order.exchange", "order.created");
                _consumer = new AsyncEventingBasicConsumer(_model);
                _consumer.Received += Consumer_Received;
                _consumerTag = _model.BasicConsume("order.queue", false, _consumer);
            });
        
        _logger.LogInformation("Message handler started");
    }


    private async Task Consumer_Received(object sender, BasicDeliverEventArgs ea)
    {
        if (await HandleEvent(ea))
        {
            _model.BasicAck(ea.DeliveryTag, false);
        }
    }

    private Task<bool> HandleEvent(BasicDeliverEventArgs ea)
    {
        var messageType = Encoding.UTF8.GetString((byte[])ea.BasicProperties.Headers["MessageType"]);
        var body = Encoding.UTF8.GetString(ea.Body.ToArray());
        return _callback.HandleMessageAsync(messageType, body);
    }

    public void Stop()
    {
        _model.BasicCancel(_consumerTag);
        _model.Close(200, "Goodbye");
        _connection.Close();
    }
}