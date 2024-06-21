using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;
using LogisticsManagement.DomainServices.Services;
using LogisticsManagement.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the DbContext on the container
var configuration = builder.Configuration;
var connectionString = configuration["WeBall:Logistics:MySqlDbConn"];
builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Register the MongoDb client
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var mongoConnString = configuration["WeBall:Logistics:MongoDbConn"];
    return new MongoClient(mongoConnString);
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<EventDbContext>();

// Register MassTransit
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(ctx);
    });
});

builder.Services.AddScoped<ILcManagement, LogisticsCompanyService>();
builder.Services.AddScoped<IEventStore, LcEventStore>();
builder.Services.AddScoped<IRepository<LogisticsCompany>, LogisticsMongoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var logistics = app.MapGroup("/api/logistics");
logistics.MapGet("/{id:guid}",
    (Guid id, ILcManagement service) => service.GetLogisticsCompanyByIdAsync(id));
logistics.MapPost("/", (ILcManagement service, LogisticsCompany logisticsCompany) =>
    service.CreateLogisticsCompanyAsync(logisticsCompany));
logistics.MapPut("/{id:guid}", (Guid id, ILcManagement service, LogisticsCompany logisticsCompany) =>
    service.UpdateLogisticsCompanyAsync(id, logisticsCompany));

app.Run();