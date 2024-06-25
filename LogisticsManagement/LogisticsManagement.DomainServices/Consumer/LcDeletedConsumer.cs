using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using LogisticsManagement.DomainServices.Interfaces;
using MassTransit;

namespace LogisticsManagement.DomainServices.Consumer;

public class LcDeletedConsumer(IRepository<LogisticsCompany> repo): IConsumer<LogisticsCompanyDeleted>
{
    public Task Consume(ConsumeContext<LogisticsCompanyDeleted> context)
    {
        var @event = context.Message;
        return repo.DeleteAsync(@event.LogisticsCompanyId); 
    }
}