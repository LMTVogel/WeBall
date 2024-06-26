using LogisticsManagement.Domain.Entities;
using LogisticsManagement.DomainServices.Interfaces;

namespace LogisticsManagement.Endpoints;

public static class LcEndpoints
{
    public static void RegisterLogisticsCompanyEndpoints(this IEndpointRouteBuilder routes)
    {
        var logistics = routes.MapGroup("/api/logistics");
        
        logistics.MapGet("/cheapest", (IShippingRatesService service) => service.GetCheapestLogisticsCompanyAsync());
        logistics.MapGet("/", (ILcManagement service) => service.GetLogisticsCompaniesAsync());
        logistics.MapGet("/{id:guid}",
            (Guid id, ILcManagement service) => service.GetLogisticsCompanyByIdAsync(id));
        logistics.MapPost("/", (ILcManagement service, LogisticsCompany logisticsCompany) =>
            service.CreateLogisticsCompanyAsync(logisticsCompany));
        logistics.MapPut("/{id:guid}", (Guid id, ILcManagement service, LogisticsCompany logisticsCompany) =>
            service.UpdateLogisticsCompanyAsync(id, logisticsCompany));
        logistics.MapDelete("/{id:guid}", (Guid id, ILcManagement service) => service.DeleteLogisticsCompanyAsync(id));
    }
}