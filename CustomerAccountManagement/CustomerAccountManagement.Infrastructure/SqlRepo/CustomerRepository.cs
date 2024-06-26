using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerAccountManagement.Infrastructure.SqlRepo;

public class CustomerRepository(SqlDbContext context) : IRepository<Customer>, ICustomerRepository
{
    public async Task<List<Customer>> GetAll()
    {
        return await context.Customers.ToListAsync();
    }

    public async Task<Customer?> GetById(Guid id)
    {
        var customer = context.Customers.FirstOrDefault(c => c.Id == id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        return customer;
    }

    public async Task Create(Customer entity)
    {
        await context.Customers.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task<Customer?> Update(Guid id, Customer customer)
    {
        var existingCustomer = await GetById(id);

        if (existingCustomer == null)
            return null;

        foreach (var property in typeof(Customer).GetProperties())
        {
            var newValue = property.GetValue(customer);
            var currentValue = property.GetValue(existingCustomer);

            if (newValue != null && newValue != currentValue)
            {
                property.SetValue(existingCustomer, newValue);
            }
        }
        
        existingCustomer.Id = id;

        context.Customers.Update(existingCustomer);
        await context.SaveChangesAsync();

        return existingCustomer;
    }

    public async Task<Customer?> Delete(Customer entity)
    {
        context.Customers.Remove(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    public async Task SaveExternalCustomers(List<Customer> customers)
    {
        foreach (var customer in customers)
        {
            await context.Customers.AddAsync(customer);
        }

        await context.SaveChangesAsync();
    }
}