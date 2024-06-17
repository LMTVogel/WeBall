using NotificationService.Application.Interfaces;
using Polly;
using RabbitMQ.Client;

namespace NotificationService.Infrastructure.Data;

public class RabbitMqConnection : IRabbitMqConnection, IDisposable
{
    private IConnection? _connection;
    public IConnection Connection => _connection!;

    public RabbitMqConnection()
    {
        InitializeRabbitMqConnection();
    }

    private void InitializeRabbitMqConnection()
    {
        Policy
            .Handle<Exception>()
            .WaitAndRetry(9, r => TimeSpan.FromSeconds(5),
                (ex, ts) => { Console.Error.WriteLine("Error connecting to rabbit mq retrying in 5sec"); })
            .Execute(() =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Port = 5672,
                    UserName = "guest",
                    Password = "guest"
                };
                _connection = factory.CreateConnection();
            });
    }

    public void Dispose()
    {
        Connection?.Dispose();
    }
}