using CustomerSupportManagement.Domain.Entities;
using CustomerSupportManagement.DomainServices.Interfaces;
using Events;
using MassTransit;

namespace CustomerSupportManagement.DomainServices.Consumers;

public class CustomerUpdatedConsumer(ISupportTicketRepo supportTicketRepo) : IConsumer<CustomerUpdated>
{
    public async Task Consume(ConsumeContext<CustomerUpdated> context)
    {
        var supportTicket = new SupportTicket
        {
            customerName = context.Message.Name,
            customerEmail = context.Message.Email
        };
        
        await supportTicketRepo.UpdateAllByUserId(context.Message.Id, supportTicket);
    }
}