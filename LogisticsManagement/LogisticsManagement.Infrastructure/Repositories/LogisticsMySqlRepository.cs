using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;

namespace LogisticsManagement.Infrastructure.Repositories;

public class LogisticsMySqlRepository(SqlDbContext ctx) : IRepository<LogisticsCompany>
{
    public LogisticsCompany? GetById(Guid id)
    {
        return ctx.LogisticsCompanies.FirstOrDefault(x => x.Id == id, null);
    }

    public IQueryable<LogisticsCompany> GetAll()
    {
        return ctx.LogisticsCompanies;
    }

    public void Add(LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.Add(entity);
        ctx.SaveChanges();
    }

    public void Update(Guid id, LogisticsCompany entity)
    {
        ctx.LogisticsCompanies.Update(entity);
    }

    public void Delete(Guid id)
    {
        var entity = ctx.LogisticsCompanies.FirstOrDefault(x => x.Id == id);
        if (entity == null) return;
        ctx.LogisticsCompanies.Remove(entity);
        ctx.SaveChanges();
    }
}