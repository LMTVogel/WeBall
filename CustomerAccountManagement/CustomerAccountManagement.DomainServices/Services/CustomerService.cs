using System.Collections.Generic;
using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Services
{
    public class CustomerService(IRepository<Customer> repository, ICustomerRepository customerRepository) : ICustomerService
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
        }

        public Task UpdateCustomer(Guid id, Customer customer)
        {
            return repository.Update(id, customer);
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customer = await repository.GetById(id);
            if (customer != null) await repository.Delete(customer);
        }
    }
}