using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;

namespace LogisticsManagement.DomainServices.Services;

public class ShippingRatesService(IRepository<LogisticsCompany> companyRepo) : IShippingRatesService
{
    public LogisticsCompany GetCheapestLogisticsCompany()
    {
        var companies = companyRepo.GetAll();
        return companies.OrderBy(x => x.ShippingRate).First();
    }

    public void UpdateShippingRates()
    {
        var companies = companyRepo.GetAll();
        foreach (var company in companies)
        {
            company.ShippingRate = new Random().Next(100, 10_000) / 100m;
            companyRepo.Update(company.Id, company);
        }
    }
}