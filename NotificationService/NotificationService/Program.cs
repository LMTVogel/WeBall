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
// dotnet user-secrets set notifications:username "test@gmail.com"
// dotnet user-secrets set notifications:password "password"
var username = builder.Configuration["notifications:username"];
var password = builder.Configuration["notifications:password"];


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Notification Worker
builder.Services.AddTransient<IEmailNotifier>((scv) =>
    new SmtpEmailNotifier("smtp.gmail.com", 587, username, password));

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

app.Run();