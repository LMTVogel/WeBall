using LogisticsManagement.Domain.Entities;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class MongoDbContext(IMongoClient client)
{
    private readonly IMongoDatabase _logisticsDb = client.GetDatabase("Logistics");
    public IMongoCollection<LogisticsCompany> LogisticsCompanies =>
        _logisticsDb.GetCollection<LogisticsCompany>("LogisticsCompanies");
}