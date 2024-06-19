using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.DomainServices.Interfaces;

public interface IShippingRatesService
{
    LogisticsCompany GetCheapestLogisticsCompany();
    void UpdateShippingRates();
}