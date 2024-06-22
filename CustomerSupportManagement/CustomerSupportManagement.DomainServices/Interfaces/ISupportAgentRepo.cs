using CustomerSupportManagement.Domain;
using CustomerSupportManagement.Domain.Entities;

namespace CustomerSupportManagement.DomainServices.Interfaces;

public interface ISupportAgentRepo
{
    Task<List<SupportAgent>> GetAll();
    Task<SupportAgent?> GetById(Guid id);
    Task Create(SupportAgent entity);
    Task<SupportAgent?> Update(SupportAgent entity);
    Task<SupportAgent?> Delete(Guid id);
}