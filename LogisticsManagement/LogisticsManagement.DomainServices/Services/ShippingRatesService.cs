using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;

namespace LogisticsManagement.DomainServices.Services;

public class ShippingRatesService(IRepository<LogisticsCompany> companyRepo) : IShippingRatesService
{
    public async Task<LogisticsCompany> GetCheapestLogisticsCompanyAsync()
    {
        var companies = await companyRepo.GetAllAsync();
        return companies.OrderBy(x => x.ShippingRate).First();
    }

    public async Task UpdateShippingRatesAsync()
    {
        var companies = await companyRepo.GetAllAsync();
        foreach (var company in companies)
        {
            company.ShippingRate = new Random().Next(100, 10_000) / 100m;
            await companyRepo.UpdateAsync(company.Id, company);
        }
    }
}