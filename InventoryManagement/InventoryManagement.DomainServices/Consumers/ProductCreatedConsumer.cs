using Events;
using InventoryManagement.Domain.Entities;
using InventoryManagement.DomainServices.Interfaces;
using MassTransit;

namespace InventoryManagement.DomainServices.Consumers;

public class ProductCreatedConsumer(IProductMongoRepository mongoRepository) : IConsumer<ProductCreated>
{
    public Task Consume(ConsumeContext<ProductCreated> context)
    {
        var product = new Product
        {
            Id = context.Message.Id,
            Name = context.Message.Name,
            Description = context.Message.Description,
            Price = context.Message.Price,
            Status = context.Message.Status,
            SupplierId = context.Message.SupplierId,
            CreatedAt = context.Message.CreatedAt,
            UpdatedAt = context.Message.UpdatedAt
        };
        
        mongoRepository.Create(product);

        return Task.CompletedTask;
    }
}