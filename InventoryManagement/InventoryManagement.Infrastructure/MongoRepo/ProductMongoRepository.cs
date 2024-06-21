using InventoryManagement.Domain.Entities;
using InventoryManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.MongoRepo;

public class ProductMongoRepository(IMongoCollection<Product> collection) : IProductMongoRepository
{
    public async Task Create(Product product)
    {
        await collection.InsertOneAsync(product);
    }

    public async Task<Product?> Update(Product product)
    {
        var existingProduct = await collection.Find(p => p.Id == product.Id).FirstOrDefaultAsync();
        if (existingProduct == null)
        {
            return null;
        }
        
        await collection.ReplaceOneAsync(p => p.Id == product.Id, product);
        return existingProduct;
    }

    public async Task<Product?> Delete(Guid id)
    {
        var product = await collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        
        if (product != null)
        {
            await collection.DeleteOneAsync(p => p.Id == id);
        }
        
        return product;
    }

    public async Task<Product?> GetById(Guid productId)
    {
        return await collection.Find(p => p.Id == productId).FirstOrDefaultAsync();
    }

    public async Task<List<Product>> GetAll()
    {
        return await collection.Find(p => true).ToListAsync();
    }
}