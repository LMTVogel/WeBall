using CustomerAccountManagement.Application.Interfaces;
using CustomerAccountManagement.Application.Services;
using CustomerAccountManagement.Domain.Entities;
using CustomerAccountManagement.Infrastructure.SqlRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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


app.Run();
