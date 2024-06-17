using CustomerAccountManagement.Application.Interfaces;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.Infrastructure.SqlRepo;

public class CustomerRepository(SqlDbContext context) : IRepository<Customer>
{
    public IQueryable<Customer> GetAll()
    {
        return context.Customers;
    }

    public Customer GetById(int id)
    {
        var customer = context.Customers.Find(id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        return customer;
    }

    public void Create(Customer entity)
    {
        context.Customers.Add(entity);
        context.SaveChanges();
    }

    public void Update(int id, Customer entity)
    {
        var customer = GetById(id);
        customer.Name = entity.Name;
        customer.Email = entity.Email;
        customer.Address = entity.Address;
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var customer = GetById(id);
        context.Customers.Remove(customer);
        context.SaveChanges();
    }
}