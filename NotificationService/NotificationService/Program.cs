using System.Reflection;
using Events;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using NotificationService.Application.Consumers;
using NotificationService.Application.Interfaces;
using NotificationService.Application.Services;
using NotificationService.Domain.Entities;
using NotificationService.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Make sure to add the secrets
// https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-8.0
// the gmail app password is generated here: https://myaccount.google.com/apppasswords
// dotnet user-secrets set WeBall:MailUsername "test@gmail.com"
// dotnet user-secrets set WeBall:MailPassword "password"


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Notification Worker
var mailUsername = builder.Configuration["WeBall:MailUsername"];
var mailPassword = builder.Configuration["WeBall:MailPassword"];

builder.Services.AddTransient<IEmailNotifier>((scv) =>
    new SmtpEmailNotifier("smtp.gmail.com", 587, mailUsername, mailPassword));

// Dependency Injection
builder.Services.AddScoped<IRepository<Notification>, NotificationSqlRepository>();

// Database context 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SqlDbContext>(opts =>
{
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>();
    x.AddConsumer<OrderUpdatedConsumer>();
    x.AddConsumer<OrderCancelledConsumer>();
    x.AddConsumer<OrderPayedConsumer>();
    x.AddConsumer<OrderShippedConsumer>();

    x.AddConsumer<PaymentFailedConsumer>();
    x.AddConsumer<PaymentPaidConsumer>();

    x.SetEndpointNameFormatter(
        new DefaultEndpointNameFormatter(prefix: Assembly.GetExecutingAssembly().GetName().Name));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["WeBall:RabbitMqHost"], "/", h =>
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

// var bus = app.Services.GetRequiredService<IBusControl>();
// var orderCreated = new OrderPayed()
// {
//     Description = "Test",
//     Name = "Test",
//     OrderId = new Guid(),
//     ClientEmail = "laurens.weterings@gmail.com"
// };
//
// await bus.Publish(orderCreated);

app.Run();