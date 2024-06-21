using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SqlRepo(SQLDbContext _context) : IRepo<Supplier>
{
    public async Task<List<Supplier>> GetAll()
    { 
        return await _context.Suppliers.ToListAsync();
    }

    public async Task<Supplier?> GetById(Guid id)
    {
        return await _context.Suppliers.FindAsync(id);
    }

    public async Task Create(Supplier supplier)
    {
        await _context.Suppliers.AddAsync(supplier);
        await _context.SaveChangesAsync();
    }

    public async Task<Supplier?> Update(Supplier supplier)
    {
        var existingSupplier = await _context.Suppliers.FindAsync(supplier.Id);

        if (existingSupplier == null)
            return null;

        foreach (var property in typeof(Supplier).GetProperties())
        {
            var newValue = property.GetValue(supplier);
            var currentValue = property.GetValue(existingSupplier);

            if (newValue != null && newValue != currentValue)
            {
                property.SetValue(existingSupplier, newValue);
            }
        }

        _context.Suppliers.Update(existingSupplier);
        await _context.SaveChangesAsync();

        return existingSupplier;
    }


    public async Task<Supplier?> Delete(Guid id)
    {
        var supplier = await _context.Suppliers.FindAsync(id);

        if (supplier == null) 
            return null;

        _context.Suppliers.Remove(supplier);
        await _context.SaveChangesAsync();

        return supplier;
    }
}