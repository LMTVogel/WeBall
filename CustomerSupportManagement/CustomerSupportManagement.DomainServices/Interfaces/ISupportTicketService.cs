using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;

namespace CustomerSupportManagement.DomainServices.Interfaces;

public interface ISupportTicketService
{
    Task<IEnumerable<SupportTicket>> GetAll();
    Task<SupportTicket> GetById(string id);
    Task Create(SupportTicket supportAgent);
    Task Update(string id, SupportTicket supportAgent);
    Task Delete(string id);
}