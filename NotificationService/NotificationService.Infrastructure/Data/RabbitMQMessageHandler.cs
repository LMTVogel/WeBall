using System.Text;
using Microsoft.Extensions.Hosting;
using NotificationService.Application.Interfaces;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NotificationService.Infrastructure.Data;

public class RabbitMqMessageHandler : IMessageHandler
{
    private IMessageHandlerCallback? _callback;
    private IModel? _model;
    private IConnection? _connection;
    private AsyncEventingBasicConsumer? _consumer;

    public void Start(string exchangeName, IMessageHandlerCallback callback)
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
                _model.QueueDeclare("test", false, false, false, null);
                _model.QueueBind("test", exchangeName, "");

                _consumer = new AsyncEventingBasicConsumer(_model);
                _consumer.Received += ConsumerReceived;
                _model.BasicConsume("test", true, _consumer);
            });
    }

    private async Task ConsumerReceived(object sender, BasicDeliverEventArgs e)
    {
        Console.WriteLine($"Message received {e.Body}");
        if (await HandleEvent(e))
        {
        }
    }

    private Task<bool> HandleEvent(BasicDeliverEventArgs e)
    {
        var messageType = Encoding.UTF8.GetString((byte[])e.BasicProperties.Headers["MessageType"]);
        var message = Encoding.UTF8.GetString(e.Body.ToArray());

        Console.WriteLine($"Message received {message}");

        return _callback.HandleMessageAsync(messageType, message);
    }

    public void Stop()
    {
        _connection?.Close();
    }
}