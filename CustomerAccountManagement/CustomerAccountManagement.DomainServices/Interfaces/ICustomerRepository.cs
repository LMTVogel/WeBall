
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Interfaces;

public interface ICustomerRepository
{
    Task SaveExternalCustomers(List<Customer> customers);
}