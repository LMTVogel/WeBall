using System.Security.Cryptography;

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

    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal ShippingRate { get; set; }
}