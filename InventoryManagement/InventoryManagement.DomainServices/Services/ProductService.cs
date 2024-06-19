using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Events;
using InventoryManagement.DomainServices.Interfaces;
using MassTransit;

namespace InventoryManagement.DomainServices.Services;

public class ProductService(IProductCommandRepository commandRepository, IProductMongoRepository mongoRepository, IPublishEndpoint serviceBus) : IProductService
{
    public IQueryable<Product> GetProducts()
    {
        return mongoRepository.GetAll();
    }

    public Product GetProductById(Guid id)
    {
        return mongoRepository.GetById(id);
    }

    public void CreateProduct(Product product)
    {
        commandRepository.Create(product);
        
        var e = new ProductCreated
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Status = product.Status,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt
        };
        
        serviceBus.Publish(e);
    }

    public void UpdateProduct(Guid id, Product product)
    {
        commandRepository.Update(id, product);
    }

    public void DeleteProduct(Guid id)
    {
        commandRepository.Delete(id);
    }
}