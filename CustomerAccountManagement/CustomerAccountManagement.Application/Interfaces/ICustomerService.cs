using System.Collections.Generic;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.Application.Interfaces
{
    public interface ICustomerService
    {
        IQueryable<Customer> GetCustomers();
        Customer GetCustomerById(int customerId);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int customerId);
    }
}