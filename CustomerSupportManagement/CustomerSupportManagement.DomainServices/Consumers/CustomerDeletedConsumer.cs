using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.Domain.Events;
using CustomerSupportManagement.DomainServices.Interfaces;
using MassTransit;

namespace CustomerSupportManagement.DomainServices.Consumers;

public class CustomerDeletedConsumer(ISupportTicketRepo supportTicketRepo) : IConsumer<CustomerUpdated>
{
    public async Task Consume(ConsumeContext<CustomerUpdated> context)
    {
        await supportTicketRepo.DeleteAllByUserId(context.Message.Id);
    }
}