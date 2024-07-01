using System.Reflection;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.DomainServices.Services;
using CustomerAccountManagement.Domain.Entities;
using CustomerAccountManagement.Infrastructure.Consumers;
using CustomerAccountManagement.Infrastructure.SqlRepo;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ICustomerIntegration, CustomerIntegrationService>();
builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

var configuration = builder.Configuration;
var connectionString = configuration["WeBall:MySQLDBConn"];
builder.Services.AddDbContext<SqlDbContext>(opts =>
{
    Policy
        .Handle<Exception>()
        .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
        .Execute(() =>
        {
            opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString),
                dbOpts => { dbOpts.EnableRetryOnFailure(100, TimeSpan.FromSeconds(10), null); });
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region MassTransit

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ExternalCustomerCreatedConsumer>();
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

#endregion

var app = builder.Build();

#region DbMigration

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SqlDbContext>();
    dbContext.Migrate();
}

#endregion

app.MapPost("/customers", async (ICustomerService customerService, Customer customer) =>
{
    await customerService.CreateCustomer(customer);
    return Results.Created($"/customers/{customer.Id}", new { code = 201, message = "Customer created successfully" });
});
app.MapPut("/customers/{id:guid}", async (ICustomerService customerService, Guid id, Customer customer) =>
{
    await customerService.UpdateCustomer(id, customer);
    return Results.Ok(new { code = 200, message = "Customer updated successfully" });
});
app.MapDelete("/customers/{id:guid}", async (ICustomerService customerService, Guid id) =>

app.MapGet("/customers/{id:guid}",
    async (ICustomerService customerService, Guid id) => { await customerService.GetCustomerById(id); });
    {
        await customerService.DeleteCustomer(id);
        return Results.Ok(new { code = 200, message = "Customer deleted successfully"});
    });
app.MapGet("/customers/{id:guid}", async (ICustomerService customerService, Guid id) => await customerService.GetCustomerById(id));
>>>>>>> main
app.MapGet("/customers/{id:guid}/order-history",
    (ICustomerService customerService, Guid id) => "History of customer with id: " + id);
app.MapPost("/customers/external", async (ICustomerIntegration integration) =>
{
    // This is a dummy method to simulate the import of external customers
    await integration.ImportExternalCustomers();
    return Results.Ok(new { code = 200, message = "External customers imported successfully" });
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();