using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.DomainServices.Interfaces;

/// <summary>
/// logistics company management
/// actions to manage logistics companies
/// </summary>
public interface ILcManagement
{
    Task<LogisticsCompany?> GetLogisticsCompanyByIdAsync(Guid id);
    Task<List<LogisticsCompany>> GetLogisticsCompaniesAsync();
    Task<LogisticsCompany> CreateLogisticsCompanyAsync(LogisticsCompany logisticsCompany);
    Task<LogisticsCompany> UpdateLogisticsCompanyAsync(Guid id, LogisticsCompany logisticsCompany);
    Task DeleteLogisticsCompanyAsync(Guid id);
}