using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Consumers;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Application.Services;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Infrastructure.Middleware;
using SupplierManagement.Infrastructure.SQLRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISupplierService, SupplierService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<ISupplierRepo, SqlSupplierRepo>();
builder.Services.AddScoped<IProductRepo, SqlProductRepo>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

builder.Services.AddMassTransit(x =>
{
    // add consumers using this following line
    x.AddConsumer<ProductCreatedConsumer>();
    x.AddConsumer<ProductDeletedConsumer>();
    x.AddConsumer<ProductUpdatedConsumer>();
    
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

var configuration = builder.Configuration;
var connectionString = configuration["WeBall:MySQLDBConn"];
builder.Services.AddDbContext<SQLDbContext>(opts =>
{
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

// # Supplier #
app.MapGet("/suppliers", async (ISupplierService supplierService) => await supplierService.GetAll());

app.MapGet("/suppliers/{id}", async (ISupplierService supplierService, string id) => await supplierService.GetById(id));

app.MapPost("/suppliers", async (ISupplierService supplierService, Supplier supplier) =>
{
    await supplierService.Create(supplier);
    return Results.Created($"/suppliers/{supplier.Id}", new { code = 201, message = "Supplier created successfully" });
});

app.MapPut("/suppliers/{id}", async (ISupplierService supplierService, string id, Supplier supplier) =>
{
    await supplierService.Update(id, supplier);
    return Results.Ok(new { code = 200, message = "Supplier updated successfully" });
});

app.MapDelete("/suppliers/{id}", async (ISupplierService supplierService, string id) =>
{
    await supplierService.Delete(id);
    return Results.Ok(new { code = 200, message = "Supplier deleted successfully" });
});

app.MapPut("/suppliers/{id}/verify", async (ISupplierService supplierService, string id) =>
{
    await supplierService.Verify(id);
    return Results.Ok(new { code = 200, message = "Supplier verified successfully" });
});

// # Product #
app.MapGet("/suppliers/{supplierId}/products", async (IProductService productService, string supplierId) => await productService.GetAllBySupplier(supplierId));

app.MapGet("/products/{productId}", async (IProductService productService, string productId) => await productService.GetById(productId));

// # Fallback #
app.MapFallback(() => Results.NotFound(new { code = 404, message = "Endpoint not found" }));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();