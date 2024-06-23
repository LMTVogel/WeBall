using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;

namespace CustomerSupportManagement.DomainServices.Interfaces;

public interface ISupportTicketRepo
{
    Task<List<SupportTicket>> GetAll();
    Task<SupportTicket?> GetById(Guid id);
    Task Create(SupportTicket entity);
    Task<SupportTicket?> Update(SupportTicket entity);
    Task<SupportTicket?> Delete(Guid id);
}