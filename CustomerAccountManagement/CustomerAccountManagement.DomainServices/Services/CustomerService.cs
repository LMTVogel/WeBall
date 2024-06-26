using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;
using Events;
using MassTransit;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(
        IRepository<Customer> repository,
        ICustomerRepository customerRepository,
        IPublishEndpoint servicebus) : ICustomerService
    {
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

            var customerCreated = new CustomerCreated
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Street = customer.Street,
                City = customer.City,
                ZipCode = customer.ZipCode
            };

            await servicebus.Publish(customerCreated);
        }

        public async Task UpdateCustomer(Guid id, Customer customer)
        {
            var updatedCustomer = await repository.Update(id, customer);

            var customerUpdated = new CustomerUpdated()
            {
                Id = updatedCustomer.Id,
                Name = updatedCustomer.Name,
                Email = updatedCustomer.Email,
                Street = updatedCustomer.Street,
                City = updatedCustomer.City,
                ZipCode = updatedCustomer.ZipCode
            };

            await servicebus.Publish(customerUpdated);
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await repository.GetById(id);
            await repository.Delete(customer);

            var customerDeleted = new CustomerDeleted() { Id = id };

            await servicebus.Publish(customerDeleted);
        }
    }
}