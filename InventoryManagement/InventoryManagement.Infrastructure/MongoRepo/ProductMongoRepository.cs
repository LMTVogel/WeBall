using InventoryManagement.Domain.Entities;
using InventoryManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.MongoRepo;

public class ProductMongoRepository(IMongoCollection<Product> collection) : IProductMongoRepository
{
    public void Create(Product product)
    {
        collection.InsertOne(product);
    }

    public void Update(Guid id, Product product)
    {
        collection.ReplaceOne(p => p.Id == id, product);
    }

    public void Delete(Guid id)
    {
        collection.DeleteOne(p => p.Id == id);
    }

    public Product GetById(Guid productId)
    {
        return collection.Find(p => p.Id == productId).FirstOrDefault();
    }

    public IQueryable<Product> GetAll()
    {
        return collection.AsQueryable();
    }
}