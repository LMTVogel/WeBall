using MongoDB.Driver;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;
using PaymentManagement.Infrastructure.Payments;
using PaymentManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Mongo Event Store

var configuration = builder.Configuration;
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var mongoConnString = configuration["WeBall:Logistics:MongoDbConn"];
    return new MongoClient(mongoConnString);
});

builder.Services.AddSingleton<EventDbContext>();
builder.Services.AddScoped<IEventStore<PaymentEvent>, PaymentEventStore>();

#endregion

#region Payment processors
builder.Services.AddTransient<AfterPaymentProcessor>();
builder.Services.AddTransient<ForwardPaymentProcessor>();
builder.Services.AddTransient<IPaymentProcessorFactory, PaymentProcessorFactory>();
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();