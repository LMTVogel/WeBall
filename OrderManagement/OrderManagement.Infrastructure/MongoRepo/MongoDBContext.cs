using MongoDB.Driver;
using OrderManagement.Domain;

namespace OrderManagement.Infrastructure;

public class MongoDbContext(IMongoClient mongoClient)
{
    private readonly IMongoDatabase _database = mongoClient.GetDatabase("OrderManagementDB");

    public IMongoCollection<Order> Orders => _database.GetCollection<Order>("Orders");
}