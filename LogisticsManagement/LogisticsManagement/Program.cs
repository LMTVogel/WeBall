using System.Reflection;
using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Consumer;
using LogisticsManagement.DomainServices.Interfaces;
using LogisticsManagement.DomainServices.Services;
using LogisticsManagement.Endpoints;
using LogisticsManagement.Infrastructure.Middleware;
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
var connectionString = configuration["WeBall:MySQLDBConn"];
builder.Services.AddDbContext<SqlDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), dbOpts =>
    {
        dbOpts.EnableRetryOnFailure(100, TimeSpan.FromSeconds(10), null);
    });
});

// Register the MongoDb client
builder.Services.AddSingleton<IMongoClient>(s =>
{
    var mongoConnString = configuration["WeBall:MongoDBConn"];
    return new MongoClient(mongoConnString);
});

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<EventDbContext>();

// Builder services
builder.Services.AddScoped<IShippingRatesService, ShippingRatesService>();

// Register MassTransit
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<LcCreatedConsumer>();
    x.AddConsumer<LcDeletedConsumer>();
    x.AddConsumer<LcUpdatedConsumer>();
    
    x.SetEndpointNameFormatter(new DefaultEndpointNameFormatter(prefix: Assembly.GetExecutingAssembly().GetName().Name));
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["WeBall:RabbitMqHost"], "/", h =>
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

#region DbMigration

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    dbContext.Migrate();
}

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();

app.RegisterLogisticsCompanyEndpoints();

app.Run();