using Events;
using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;
using MassTransit;

namespace LogisticsManagement.DomainServices.Consumer;

public class LcSyncConsumer(IRepository<LogisticsCompany> repo) : IConsumer<LogisticsCompanySync>
{
    public Task Consume(ConsumeContext<LogisticsCompanySync> context)
    {
        var @event = context.Message;
        var logisticsCompany = new LogisticsCompany();
        logisticsCompany.Apply(@event);
        return repo.UpdateAsync(@event.LogisticsCompanyId, logisticsCompany);
    }
}