using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Events;
using InventoryManagement.Domain.Exceptions;
using InventoryManagement.DomainServices.Interfaces;
using MassTransit;

namespace InventoryManagement.DomainServices.Services;

public class ProductService(IProductCommandRepository commandRepository, IProductMongoRepository mongoRepository, IPublishEndpoint serviceBus) : IProductService
{
    public async Task<IEnumerable<Product>> GetProducts()
    {
        var products = await mongoRepository.GetAll();

        if (products.Count == 0)
            throw new HttpException("No products found", 404);
        
        return products;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        var product = await mongoRepository.GetById(id);

        if (product == null)
            throw new HttpException("Product not found", 404);
        
        return product;
    }

    public async Task CreateProduct(Product product)
    {
        var createdProduct = await commandRepository.Create(product);
        
        var e = new ProductCreated
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Description = createdProduct.Description,
            Price = createdProduct.Price,
            Status = createdProduct.Status,
            SupplierId = createdProduct.SupplierId,
            CreatedAt = createdProduct.CreatedAt,
            UpdatedAt = createdProduct.UpdatedAt
        };
        
        await serviceBus.Publish(e);
    }

    public async Task UpdateProduct(Guid id, Product product)
    {
        var existingProduct = await commandRepository.GetById(id);
        
        if (existingProduct == null)
            throw new HttpException("Product not found", 404);

        foreach (var property in typeof(Product).GetProperties())
        {
            var newValue = property.GetValue(product);
            var currentValue = property.GetValue(existingProduct);

            if (newValue != null && newValue != currentValue)
            {
                property.SetValue(existingProduct, newValue);
            }
        }
        existingProduct.Id = id;
        await commandRepository.Update(existingProduct);
        
        var e = new ProductUpdated
        {
            Id = id,
            Name = existingProduct.Name,
            Description = existingProduct.Description,
            Price = existingProduct.Price,
            Status = existingProduct.Status,
            SupplierId = existingProduct.SupplierId,
            CreatedAt = existingProduct.CreatedAt,
            UpdatedAt = existingProduct.UpdatedAt
        };
        
        await serviceBus.Publish(e);
    }

    public async Task DeleteProduct(Guid id)
    {
        var product = await commandRepository.GetById(id);
        if (product == null)
            throw new HttpException("Product not found", 404);
        
        await commandRepository.Delete(product);
        
        await serviceBus.Publish(new ProductDeleted { Id = id });
    }
}