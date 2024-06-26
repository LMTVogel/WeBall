using Events;
using InventoryManagement.DomainServices.Interfaces;
using MassTransit;

namespace InventoryManagement.DomainServices.Consumers;

public class ProductDeletedConsumer(IProductMongoRepository mongoRepository) : IConsumer<ProductDeleted>
{
    public Task Consume(ConsumeContext<ProductDeleted> context)
    {
        mongoRepository.Delete(context.Message.Id);

        return Task.CompletedTask;
    }
}