using NotificationService.Api.Controllers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency Injection
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var orderGroup = app.MapGroup("/api/orders");
orderGroup.MapGet("/", (IOrderService orderService) => orderService.GetOrders());
orderGroup.MapGet("/{id}", (IOrderService orderService, int id) => orderService.GetOrderById(id));
orderGroup.MapPost("/", (IOrderService orderService, Order order) => orderService.CreateOrder(order));
orderGroup.MapPut("/{id}", (IOrderService orderService, int id, Order order) => orderService.UpdateOrder(id, order));
orderGroup.MapDelete("/{id}", (IOrderService orderService, int id) => orderService.DeleteOrder(id));

app.Run();

