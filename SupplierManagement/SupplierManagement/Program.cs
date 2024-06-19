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

app.UseMiddleware<ExceptionMiddleware>();

// # Supplier #
app.MapGet("/supplier", (ISupplierService supplierService) => supplierService.GetAll());
app.MapPost("/supplier", (ISupplierService supplierService, Supplier supplier) => supplierService.Create(supplier));
app.MapPut("/supplier/{id}", (ISupplierService supplierService, string id, Supplier supplier) => supplierService.Update(id, supplier));
app.MapDelete("/supplier/{id}", (ISupplierService supplierService, string id) => supplierService.Delete(id));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();