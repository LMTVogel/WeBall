using CustomerAccountManagement.DomainServices.Interfaces;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.Infrastructure.SqlRepo;

public class CustomerRepository(SqlDbContext context) : IRepository<Customer>, ICustomerRepository
{
    public IQueryable<Customer> GetAll()
    {
        return context.Customers;
    }

    public Customer GetById(Guid id)
    {
        var customer = context.Customers.Find(id);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }
        return customer;
    }
    
    public Customer GetCustomerByEmail(string email)
    {
        var customer = context.Customers.FirstOrDefault(c => c.Email == email);
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

    public void Update(Guid id, Customer entity)
    {
        var customer = GetById(id);
        customer.Name = entity.Name;
        customer.Email = entity.Email;
        customer.Street = entity.Street;
        customer.City = entity.City;
        customer.ZipCode = entity.ZipCode;
        context.Customers.Update(customer);
        context.SaveChanges();
    }

    public void Delete(Customer entity)
    {
        context.Customers.Remove(entity);
        context.SaveChanges();
    }
}