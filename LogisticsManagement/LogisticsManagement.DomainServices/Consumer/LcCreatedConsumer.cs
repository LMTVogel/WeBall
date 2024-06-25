using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using LogisticsManagement.DomainServices.Interfaces;
using MassTransit;

namespace LogisticsManagement.DomainServices.Consumer;

public class LcCreatedConsumer(IRepository<LogisticsCompany> repo) : IConsumer<LogisticsCompanyCreated>
{
    public Task Consume(ConsumeContext<LogisticsCompanyCreated> context)
    {
        var @event = context.Message;
        var logisticsCompany = new LogisticsCompany();
        logisticsCompany.Apply(@event);
        return repo.CreateAsync(logisticsCompany);
    }
}