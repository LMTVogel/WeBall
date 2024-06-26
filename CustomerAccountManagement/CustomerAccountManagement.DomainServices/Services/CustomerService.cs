using System.Collections.Generic;
using System.Globalization;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;
using System.Threading.Tasks;
using CsvHelper;
using Events;
using MassTransit;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(IRepository<Customer> repository, ICustomerRepository customerRepository, IPublishEndpoint servicebus) : ICustomerService
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await repository.GetAll();
        }

        public async Task<Customer?> GetCustomerById(Guid customerId)
        {
            return await repository.GetById(customerId);
        }

        public async Task CreateCustomer(Customer customer)
        {
            await repository.Create(customer);
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

        public Task UpdateCustomer(Guid id, Customer customer)
        {
            return repository.Update(id, customer);
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

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await repository.GetById(id);
            if (customer != null) await repository.Delete(customer);
            var customer = repository.GetById(id);
            repository.Delete(customer);

            var customerDeleted = new CustomerDeleted() { Id = id };

            servicebus.Publish(customerDeleted);
        }
    }
}