using MongoDB.Driver;
using OrderManagement.Domain.Events;

namespace OrderManagement.Infrastructure;

public class EventDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _eventDb = client.GetDatabase("EventDb");
    
    public IMongoCollection<OrderEvent> OrderEvents => _eventDb.GetCollection<OrderEvent>("OrderEvents");
}