using MassTransit;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Events;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

// Notification Worker
builder.Services.AddTransient<IEmailNotifier>((scv) =>
    new SmtpEmailNotifier("smtp.gmail.com", 587, "username", "password"));

// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.AddConsumer<OrderUpdatedConsumer>();
    x.AddConsumer<OrderCancelledConsumer>();
    x.AddConsumer<OrderPayedConsumer>();
    x.AddConsumer<OrderShippedConsumer>();
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var orderGroup = app.MapGroup("/api/orders");
orderGroup.MapGet("/", (IOrderService orderService) => orderService.GetOrders());
orderGroup.MapGet("/{id:guid}", (IOrderService orderService, Guid id) => orderService.GetOrderById(id));
orderGroup.MapPost("/", (IOrderService orderService, Order order) => orderService.CreateOrder(order));
orderGroup.MapPut("/{id:guid}",
    (IOrderService orderService, Guid id, Order order) => orderService.UpdateOrder(id, order));
orderGroup.MapDelete("/{id:guid}", (IOrderService orderService, Guid id) => orderService.DeleteOrder(id));

app.Run();