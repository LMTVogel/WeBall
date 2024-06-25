using System.Collections.Generic;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;
using CustomerAccountManagement.Domain.Events;
using MassTransit;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(IRepository<Customer> repository, ICustomerRepository customerRepository, IPublishEndpoint servicebus) : ICustomerService
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
            
            var customerCreated = new CustomerCreated
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Street = customer.Street,
                City = customer.City,
                ZipCode = customer.ZipCode
            };
            
            servicebus.Publish(customerCreated);
        }

        public void UpdateCustomer(Guid id, Customer customer)
        {
            var updatedCustomer = repository.Update(id, customer);
            
            var customerUpdated = new CustomerUpdated()
            {
                Id = updatedCustomer.Id,
                Name = updatedCustomer.Name,
                Email = updatedCustomer.Email,
                Street = updatedCustomer.Street,
                City = updatedCustomer.City,
                ZipCode = updatedCustomer.ZipCode
            };
            
            servicebus.Publish(customerUpdated);
        }

        public void DeleteCustomer(Guid id)
        {
            var customer = repository.GetById(id);
            repository.Delete(customer);
            
            var customerDeleted = new CustomerDeleted() { Id = id };
            
            servicebus.Publish(customerDeleted);
        }
    }
}