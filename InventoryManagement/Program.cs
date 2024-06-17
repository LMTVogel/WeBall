using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var rabbitMqConfig = configuration.GetSection("RabbitMQ");
var rabbitMqHostName = rabbitMqConfig["HostName"];
var rabbitMqUserName = rabbitMqConfig["UserName"];
var rabbitMqPassword = rabbitMqConfig["Password"];

var factory = new ConnectionFactory()
{
    HostName = rabbitMqHostName,
    UserName = rabbitMqUserName,
    Password = rabbitMqPassword
};

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare("inventory", ExchangeType.Topic);
var routingKey = "inventory.*";
var queueName = channel.QueueDeclare().QueueName;
channel.QueueBind(queueName, "inventory", routingKey);

app.Run();
