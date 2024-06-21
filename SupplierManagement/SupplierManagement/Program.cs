using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Application.Services;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Infrastructure.Middleware;
using SupplierManagement.Infrastructure.SQLRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IRepo<Supplier>, SqlRepo>();

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

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
app.MapGet("/supplier", async (ISupplierService supplierService) => await supplierService.GetAll());
app.MapGet("/supplier/{id}", async (ISupplierService supplierService, string id) => await supplierService.GetById(id));
app.MapPost("/supplier", async (ISupplierService supplierService, Supplier supplier) =>
{
    await supplierService.Create(supplier);
    
    return Results.Ok(new
    {
        code = "201",
        message = "Supplier created successfully"
    });
});
app.MapPut("/supplier/{id}", async (ISupplierService supplierService, string id, Supplier supplier) =>
{
    await supplierService.Update(id, supplier);
    
    return Results.Ok(new
    {
        code = "200",
        message = "Supplier updated successfully"
    });
});
app.MapDelete("/supplier/{id}", async (ISupplierService supplierService, string id) =>
{
    await supplierService.Delete(id);
    
    return Results.Ok(new
    {
        code = "200",
        message = "Supplier deleted successfully"
    });
});

app.MapFallback(() => Results.NotFound(new
{
    code = "404",
    message = "Endpoint not found"
}));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();