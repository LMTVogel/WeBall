using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.DomainServices.Services;
using CustomerAccountManagement.Domain.Entities;
using CustomerAccountManagement.Infrastructure.SqlRepo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var configuration = builder.Configuration;
var connectionString = configuration["WeBall:MySQLDBConn"];
builder.Services.AddDbContext<SqlDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/register", (ICustomerService customerService, Customer customer) =>
{
    customerService.CreateCustomer(customer);
});
app.MapPut("/update/{id:guid}", (ICustomerService customerService, Guid id, Customer customer) =>
{
    customerService.UpdateCustomer(id, customer);
});
app.MapDelete("/delete/{id:guid}", (ICustomerService customerService, Guid id) =>
{
    customerService.DeleteCustomer(id);
});
app.MapGet("/profile/{id:guid}", (ICustomerService customerService, Guid id) =>
{
    customerService.GetCustomerById(id);
});
app.MapGet("/profile/{id:guid}/order-history", (ICustomerService customerService, Guid id) => "History of customer with id: " + id);
app.MapPost("/customers", (ICustomerService customerService) =>
{
    // This is a dummy method to simulate the import of external customers
    customerService.ImportExternalCustomers();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();
