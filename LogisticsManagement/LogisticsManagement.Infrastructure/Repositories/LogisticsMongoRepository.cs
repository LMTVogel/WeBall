using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class LogisticsMongoRepository(MongoDbContext ctx) : IRepository<LogisticsCompany>
{
    public LogisticsCompany? GetById(Guid id)
    {
        return ctx.LogisticsCompanies.Find(x => x.Id == id).FirstOrDefault();
    }

    public IQueryable<LogisticsCompany> GetAll()
    {
        return ctx.LogisticsCompanies.AsQueryable();
    }

    public void Add(LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.InsertOne(entity);
    }

    public void Update(Guid id, LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.ReplaceOne(x => x.Id == id, entity);
    }

    public void Delete(Guid id)
    {
        ctx.LogisticsCompanies.DeleteOne(x => x.Id == id);
    }
}