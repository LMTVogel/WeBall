using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SqlRepo(SQLDbContext _context) : IRepo<Supplier>
{
    public IQueryable<Supplier> GetAll()
    {
        return _context.Suppliers;
    }

    public Supplier? GetById(Guid id)
    {
        return _context.Suppliers.Find(id);
    }

    public void Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    public Supplier? Update(Supplier supplier)
    {
        var existingSupplier = _context.Suppliers.Find(supplier.Id);

        if (existingSupplier == null) 
            return null;

        _context.Entry(existingSupplier).CurrentValues.SetValues(supplier);
        _context.SaveChanges();

        return existingSupplier;
    }


    public void Delete(Guid id)
    {
        Supplier customer = new Supplier() { Id = id };
        _context.Suppliers.Remove(customer);
        _context.SaveChanges();
    }
}