using Events;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class EventDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _eventDb = client.GetDatabase("LogisticsEvents");

    public IMongoCollection<LogisticsCompanyEvent> Events =>
        _eventDb.GetCollection<LogisticsCompanyEvent>("Events");
}