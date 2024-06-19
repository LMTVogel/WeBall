using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SqlRepo(SQLDbContext _context) : IRepo<Supplier>
{
    public IQueryable<Supplier> GetAll()
    {
        return _context.Suppliers;
    }

    public Supplier? GetById(string id)
    {
        return _context.Suppliers.Find(Guid.Parse(id));
    }

    public void Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    public void Update(string id, Supplier supplier)
    {
        _context.Suppliers.Find(Guid.Parse(id));
        _context.Suppliers.Update(supplier);
        _context.SaveChanges();
    }


    public void Delete(string id)
    {
        Supplier customer = new Supplier() { Id = Guid.Parse(id) };
        _context.Suppliers.Remove(customer);
        _context.SaveChanges();
    }
}