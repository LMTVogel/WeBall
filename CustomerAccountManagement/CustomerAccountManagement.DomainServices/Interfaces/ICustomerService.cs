using System.Collections.Generic;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetCustomers();
        Customer GetCustomerById(Guid id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Guid id, Customer customer);
        void DeleteCustomer(Guid id);
    }
}