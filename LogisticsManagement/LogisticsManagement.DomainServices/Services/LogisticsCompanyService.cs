using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using LogisticsManagement.DomainServices.Interfaces;

namespace LogisticsManagement.DomainServices.Services;

public class LogisticsCompanyService(IRepository<LogisticsCompany> repo, IEventStore eventStore)
    : ILcManagement
{
    public async Task<LogisticsCompany?> GetLogisticsCompanyByIdAsync(Guid id)
    {
        var events = await eventStore.ReadAsync<LogisticsCompanyCreated>(id);
        var logisticsCompany = new LogisticsCompany();
        foreach (var @event in events)
        {
            switch (@event)
            {
                case LogisticsCompanyCreated created:
                    logisticsCompany.Id = created.LogisticsCompanyId;
                    logisticsCompany.Name = created.Name;
                    logisticsCompany.ShippingRate = created.ShippingRate;
                    break;
                case LogisticsCompanyUpdated updated:
                    logisticsCompany.ShippingRate = updated.ShippingRate;
                    break;
                case LogisticsCompanyDeleted _:
                    return null;
            }
        }

        return logisticsCompany;
    }

    public async Task<IQueryable<LogisticsCompany>> GetLogisticsCompaniesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<LogisticsCompany> CreateLogisticsCompanyAsync(LogisticsCompany logisticsCompany)
    {
        var @event = new LogisticsCompanyCreated()
        {
            LogisticsCompanyId = Guid.NewGuid(),
            Name = logisticsCompany.Name,
            ShippingRate = logisticsCompany.ShippingRate,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);

        logisticsCompany.Id = @event.LogisticsCompanyId;

        return logisticsCompany;
    }

    public async Task<LogisticsCompany> UpdateLogisticsCompanyAsync(Guid id, LogisticsCompany logisticsCompany)
    {
        var @event = new LogisticsCompanyUpdated()
        {
            LogisticsCompanyId = logisticsCompany.Id,
            ShippingRate = logisticsCompany.ShippingRate,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);

        return (await GetLogisticsCompanyByIdAsync(id))!;
    }

    public async Task DeleteLogisticsCompanyAsync(Guid id)
    {
        var @event = new LogisticsCompanyDeleted()
        {
            LogisticsCompanyId = id,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);
    }
}