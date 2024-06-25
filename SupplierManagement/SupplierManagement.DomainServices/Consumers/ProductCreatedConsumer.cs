using MassTransit;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Events;

namespace SupplierManagement.Application.Consumers;

public class ProductCreatedConsumer(IProductRepo productRepo) : IConsumer<ProductCreated>
{
    public Task Consume(ConsumeContext<ProductCreated> context)
    {
        var product = new Product
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            SupplierId = context.Message.SupplierId
        };

        productRepo.Create(product);
        
        return Task.CompletedTask;
    }
}