using MassTransit;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Events;

namespace SupplierManagement.Application.Consumers;

public class ProductUpdatedConsumer(IProductRepo productRepo): IConsumer<ProductUpdated>
{
    public Task Consume(ConsumeContext<ProductUpdated> context)
    {
        var product = new Product
        {
            Id = context.Message.SupplierId,
            Name = context.Message.Name,
            SupplierId = context.Message.SupplierId
        };
        
        productRepo.Update(product);

        return Task.CompletedTask;
    }
}