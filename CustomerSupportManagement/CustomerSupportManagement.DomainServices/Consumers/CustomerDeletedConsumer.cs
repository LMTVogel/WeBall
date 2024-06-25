using CustomerSupportManagement.DomainServices.Interfaces;
using Events;
using MassTransit;

namespace CustomerSupportManagement.DomainServices.Consumers;

public class CustomerDeletedConsumer(ISupportTicketRepo supportTicketRepo) : IConsumer<CustomerUpdated>
{
    public async Task Consume(ConsumeContext<CustomerUpdated> context)
    {
        await supportTicketRepo.DeleteAllByUserId(context.Message.Id);
    }
}