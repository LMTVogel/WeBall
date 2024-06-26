using CustomerAccountManagement.Domain.Entities;
using CustomerAccountManagement.DomainServices.Interfaces;
using Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace CustomerAccountManagement.Infrastructure.Consumers;

public class ExternalCustomerCreatedConsumer(
    IRepository<Customer> customerRepository,
    ILogger<ExternalCustomerCreatedConsumer> logger)
    : IConsumer<ExternalCustomerCreated>
{
    public async Task Consume(ConsumeContext<ExternalCustomerCreated> context)
    {
        logger.LogInformation("Received external customer created event");
        var customer = new Customer
        {
            Name = context.Message.Name,
            Email = context.Message.Email,
            Street = context.Message.Street,
            City = context.Message.City,
            ZipCode = context.Message.ZipCode
        };

        await customerRepository.Create(customer);
    }
}