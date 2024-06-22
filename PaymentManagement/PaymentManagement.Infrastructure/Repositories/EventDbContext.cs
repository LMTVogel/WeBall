using MongoDB.Driver;
using PaymentManagement.Domain.Events;

namespace PaymentManagement.Infrastructure.Repositories;

public class EventDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _eventDb = client.GetDatabase("PaymentEvents");
    
    public IMongoCollection<PaymentEvent> PaymentEvents => _eventDb.GetCollection<PaymentEvent>("Events");
}