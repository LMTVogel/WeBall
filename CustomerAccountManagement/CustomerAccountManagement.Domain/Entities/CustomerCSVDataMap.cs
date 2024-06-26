using CsvHelper.Configuration;

namespace CustomerAccountManagement.Domain.Entities;

public sealed class CustomerCSVDataMap : ClassMap<CustomerCSVData>
{
    public CustomerCSVDataMap()
    {
        Map(m => m.CompanyName).Name("Company Name");
        Map(m => m.FirstName).Name("First Name");
        Map(m => m.LastName).Name("Last Name");
        Map(m => m.PhoneNumber).Name("Phone Number");
        Map(m => m.Address).Name("Address");
    }
}