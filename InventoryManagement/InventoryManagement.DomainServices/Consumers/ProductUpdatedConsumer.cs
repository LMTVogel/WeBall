using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Events;
using InventoryManagement.DomainServices.Interfaces;
using MassTransit;

namespace InventoryManagement.DomainServices.Consumers;

public class ProductUpdatedConsumer(IProductMongoRepository mongoRepository) : IConsumer<ProductUpdated>
{
    public Task Consume(ConsumeContext<ProductUpdated> context)
    {
        var product = new Product
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            Description = context.Message.Description,
            Price = context.Message.Price,
            Status = context.Message.Status,
            CreatedAt = context.Message.CreatedAt,
            UpdatedAt = context.Message.UpdatedAt
        };
        
        mongoRepository.Update(product);

        return Task.CompletedTask;
    }
}