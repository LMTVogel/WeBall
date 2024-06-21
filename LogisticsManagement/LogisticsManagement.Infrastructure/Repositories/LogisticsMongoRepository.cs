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
        return await ctx.LogisticsCompanies.Find(_ => true).ToListAsync();
    }

    public async Task CreateAsync(LogisticsCompany entity)
    {
        await ctx.LogisticsCompanies.InsertOneAsync(entity);
    }

    public async Task<LogisticsCompany?> UpdateAsync(Guid id, LogisticsCompany entity)
    {
        var logisticsCompany = await ctx.LogisticsCompanies.Find(x => x.Id == id).FirstOrDefaultAsync();
        if (logisticsCompany == null) return null;

        foreach (var prop in typeof(LogisticsCompany).GetProperties())
        {
            if (prop.GetValue(entity) != null)
            {
                prop.SetValue(logisticsCompany, prop.GetValue(entity));
            }
        }

        var filter = Builders<LogisticsCompany>.Filter.Eq(lc => lc.Id, id);
        var update = Builders<LogisticsCompany>.Update
            .Set(lc => lc.Name, logisticsCompany.Name)
            .Set(lc => lc.ShippingRate, logisticsCompany.ShippingRate);

        await ctx.LogisticsCompanies.UpdateOneAsync(filter, update);

        return logisticsCompany;
    }

    public async Task DeleteAsync(Guid id)
    {
        await ctx.LogisticsCompanies.DeleteOneAsync(x => x.Id == id);
    }
}