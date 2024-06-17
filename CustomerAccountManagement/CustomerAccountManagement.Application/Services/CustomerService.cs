using System.Collections.Generic;
using CustomerAccountManagement.Application.Interfaces;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.Application.Services
{
    public class CustomerService(IRepository<Customer> customerRepository) : ICustomerService
    {
        public IQueryable<Customer> GetCustomers()
        {
            return customerRepository.GetAll();
        }

        public Customer GetCustomerById(int customerId)
        {
            return customerRepository.GetById(customerId);
        }

        public void CreateCustomer(Customer customer)
        {
            customerRepository.Create(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerRepository.Update(customer.Id, customer);
        }

        public void DeleteCustomer(int customerId)
        {
            customerRepository.Delete(customerId);
        }
    }
}