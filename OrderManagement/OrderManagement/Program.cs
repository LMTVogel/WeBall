using MongoDB.Driver;
using OrderManagement.Domain;
using OrderManagement.DomainServices;
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

// Adding the MongoDbContext
builder.Services.AddSingleton<MongoDbContext>();

// Adding the repositories
builder.Services.AddScoped<IOrderRepository, MongoOrderRepository>();

// Adding the services
builder.Services.AddScoped<IOrderService, OrderService>();

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