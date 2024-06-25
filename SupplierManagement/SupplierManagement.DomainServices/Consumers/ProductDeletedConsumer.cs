using Events;
using MassTransit;
using SupplierManagement.Application.Interfaces;

namespace SupplierManagement.Application.Consumers;

public class ProductDeletedConsumer(IProductRepo productRepo): IConsumer<ProductDeleted>
{
    public Task Consume(ConsumeContext<ProductDeleted> context)
    {
        productRepo.Delete(context.Message.Id);
        
        return Task.CompletedTask;
    }
}