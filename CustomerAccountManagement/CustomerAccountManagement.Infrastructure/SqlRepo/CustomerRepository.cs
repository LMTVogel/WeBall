﻿using CustomerAccountManagement.DomainServices.Interfaces;
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

    public async Task<Customer?> Update(Guid id, Customer entity)
    {
        var customer = await GetById(id);
        if (customer != null)
        {
            customer.Name = entity.Name;
            customer.Email = entity.Email;
            customer.Street = entity.Street;
            customer.City = entity.City;
            customer.ZipCode = entity.ZipCode;
            context.Customers.Update(customer);
        }

        await context.SaveChangesAsync();
        return customer;
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