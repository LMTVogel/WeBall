using System.Reflection;
using MassTransit;
using MongoDB.Driver;
using OrderManagement.Domain;
using OrderManagement.DomainServices;
using OrderManagement.DomainServices.Consumers;
using OrderManagement.Endpoints;
using OrderManagement.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddSingleton<IMongoClient>(s =>
{
    var configuration = builder.Configuration;
    var connectionString = configuration["WeBall:MongoDBConn"];
    return new MongoClient(connectionString);
});

// Adding the DbContexts
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<EventDbContext>();

// Adding the repositories
builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();

// Adding the services
builder.Services.AddScoped<IOrderService, OrderService>();

// Register MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.AddConsumer<OrderUpdatedConsumer>();

    x.AddConsumer<PaymentFailedConsumer>();
    x.AddConsumer<PaymentPaidConsumer>();

    x.SetEndpointNameFormatter(
        new DefaultEndpointNameFormatter(prefix: Assembly.GetExecutingAssembly().GetName().Name));

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration["WeBall:RabbitMqHost"], "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        config.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Register the Order endpoints
app.RegisterOrderEndpoints();

app.Run();