using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class MongoDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _logisticsDb = client.GetDatabase("Logistics");
    private readonly IMongoDatabase _eventDb = client.GetDatabase("LogisticsEvents");
    public IMongoCollection<Event> Events =>
        _eventDb.GetCollection<Event>("Events");

    public IMongoCollection<LogisticsCompany> LogisticsCompanies =>
        _logisticsDb.GetCollection<LogisticsCompany>("LogisticsCompanies");
}
