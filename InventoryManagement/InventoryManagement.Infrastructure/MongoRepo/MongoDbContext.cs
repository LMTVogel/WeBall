using InventoryManagement.Domain.Entities;
using MongoDB.Driver;

namespace InventoryManagement.Infrastructure.MongoRepo;

public class MongoDbContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("InventoryManagement");
    
    public IMongoCollection<Product> Products => _database.GetCollection<Product>("Products");
}