using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;

namespace CustomerSupportManagement.DomainServices.Interfaces;

public interface ISupportAgentService
{
    Task<IEnumerable<SupportAgent>> GetAll();
    Task<SupportAgent> GetById(string id);
    Task Create(SupportAgent supportAgent);
    Task Update(string id, SupportAgent supportAgent);
    Task Delete(string id);
}