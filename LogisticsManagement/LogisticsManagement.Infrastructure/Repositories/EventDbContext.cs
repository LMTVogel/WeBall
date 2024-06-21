using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class EventDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _eventDb = client.GetDatabase("LogisticsEvents");

    public IMongoCollection<Event> Events =>
        _eventDb.GetCollection<Event>("Events");
}