using System.Collections.Generic;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(IRepository<Customer> repository, ICustomerRepository customerRepository) : ICustomerService
    {
        public IQueryable<Customer> GetCustomers()
        {
            return repository.GetAll();
        }

        public Customer GetCustomerById(Guid customerId)
        {
            return repository.GetById(customerId);
        }

        public void CreateCustomer(Customer customer)
        {
            repository.Create(customer);
        }

        public void UpdateCustomer(Guid id, Customer customer)
        {
            repository.Update(id, customer);
        }

        public void DeleteCustomer(Guid id)
        {
            var customer = repository.GetById(id);
            repository.Delete(customer);
        }
    }
}