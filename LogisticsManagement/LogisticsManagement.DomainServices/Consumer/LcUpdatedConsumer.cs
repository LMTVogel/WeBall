using Events;
using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;
using MassTransit;

namespace LogisticsManagement.DomainServices.Consumer;

public class LcUpdatedConsumer(IRepository<LogisticsCompany> repo) : IConsumer<LogisticsCompanyUpdated>
{
    public Task Consume(ConsumeContext<LogisticsCompanyUpdated> context)
    {
        var @event = context.Message;
        var logisticsCompany = new LogisticsCompany();
        logisticsCompany.Apply(@event);
        return repo.UpdateAsync(@event.LogisticsCompanyId, logisticsCompany);
    }
}