using MassTransit;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Data;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IMessageProducer, RabbitMqMessageProducer>();

// Notification Worker
builder.Services.AddTransient<IMessageHandler, RabbitMqMessageHandler>();
builder.Services.AddTransient<IEmailNotifier>((scv) =>
    new SmtpEmailNotifier("smtp.gmail.com", 587, "username", "password"));
builder.Services.AddHostedService<NotificationWorker>();


// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumers(typeof(Program).Assembly);
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("order-created", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//TODO: EXTRACT TO NotificationService.API controllers
var orderGroup = app.MapGroup("/api/orders");
orderGroup.MapGet("/", (IOrderService orderService) => orderService.GetOrders());
orderGroup.MapGet("/{id:int}", (IOrderService orderService, int id) => orderService.GetOrderById(id));
orderGroup.MapPost("/", (IOrderService orderService, Order order) => orderService.CreateOrder(order));
orderGroup.MapPut("/{id:int}", (IOrderService orderService, int id, Order order) => orderService.UpdateOrder(id, order));
orderGroup.MapDelete("/{id:int}", (IOrderService orderService, int id) => orderService.DeleteOrder(id));

app.Run();