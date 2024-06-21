using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

public class LogisticsMongoRepository(MongoDbContext ctx) : IRepository<LogisticsCompany>
{
    public async Task<LogisticsCompany?> GetByIdAsync(Guid id)
    {
        return await ctx.LogisticsCompanies.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<LogisticsCompany>> GetAllAsync()
    {
        // return ctx.LogisticsCompanies.AsQueryable();
        return await ctx.LogisticsCompanies.Find(_ => true).ToListAsync();
    }

    public Task AddAsync(LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.InsertOne(entity);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Guid id, LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.ReplaceOne(x => x.Id == id, entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Guid id)
    {
        ctx.LogisticsCompanies.DeleteOne(x => x.Id == id);
        return Task.CompletedTask;
    }
}