using System.Security.Cryptography;
using LogisticsManagement.Domain.Events;

namespace LogisticsManagement.Domain.Entities;

public class LogisticsCompany
{
    public LogisticsCompany(string name, decimal shippingRate)
    {
        Id = Guid.NewGuid();
        Name = name;
        ShippingRate = shippingRate;
    }

    public LogisticsCompany(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        ShippingRate = RandomNumberGenerator.GetInt32(100, 1000) / 100m;
    }

    public LogisticsCompany()
    {
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal ShippingRate { get; set; }

    private void Apply(LogisticsCompanyCreated @event)
    {
        Id = @event.LogisticsCompanyId;
        Name = @event.Name;
        ShippingRate = @event.ShippingRate;
    }

    private void Apply(LogisticsCompanyUpdated @event)
    {
        ShippingRate = @event.ShippingRate;
    }

    private void Apply(LogisticsCompanyDeleted @event)
    {
        Id = @event.LogisticsCompanyId;
    }

    public void Apply(Event @event)
    {
        switch (@event)
        {
            case LogisticsCompanyCreated created:
                Apply(created);
                break;
            case LogisticsCompanyUpdated updated:
                Apply(updated);
                break;
            case LogisticsCompanyDeleted deleted:
                Apply(deleted);
                break;
        }
    }
}