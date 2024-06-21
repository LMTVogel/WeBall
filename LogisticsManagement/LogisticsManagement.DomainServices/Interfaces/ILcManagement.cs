using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;

namespace LogisticsManagement.DomainServices.Interfaces;

/// <summary>
/// logistics company management
/// actions to manage logistics companies
/// </summary>
public interface ILcManagement
{
    Task<LogisticsCompany?> GetLogisticsCompanyByIdAsync(Guid id);
    Task<IQueryable<LogisticsCompany>> GetLogisticsCompaniesAsync();
    Task<LogisticsCompany> CreateLogisticsCompanyAsync(LogisticsCompany logisticsCompany);
    Task<LogisticsCompany> UpdateLogisticsCompanyAsync(Guid id, LogisticsCompany logisticsCompany);
    Task DeleteLogisticsCompanyAsync(Guid id);
}