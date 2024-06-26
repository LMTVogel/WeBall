using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.DomainServices.Interfaces;

public interface IShippingRatesService
{
    Task<LogisticsCompany?> GetCheapestLogisticsCompanyAsync();
    Task UpdateShippingRatesAsync();
}