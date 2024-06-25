using System.Collections.Generic;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetCustomers();
        Task<Customer?> GetCustomerById(Guid id);
        Task CreateCustomer(Customer customer);
        Task UpdateCustomer(Guid id, Customer customer);
        Task DeleteCustomer(Guid id);
        Task ImportExternalCustomers();
    }
}