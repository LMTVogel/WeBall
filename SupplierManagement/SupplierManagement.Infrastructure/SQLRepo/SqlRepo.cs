using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SqlRepo(SQLDbContext _context) : IRepo<Supplier>
{
    public IQueryable<Supplier> GetAll()
    {
        return _context.Suppliers;
    }

    public Supplier GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Create(Supplier supplier)
    {
        _context.Suppliers.Add(supplier);
        _context.SaveChanges();
    }

    public void Update(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}