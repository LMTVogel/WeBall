using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SQLSupplierRepo : ISupplierRepo
{
    public IQueryable<Supplier> GetAll()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Supplier> GetSuppliers()
    {
        throw new NotImplementedException();
    }

    public Supplier GetSupplierById(int id)
    {
        throw new NotImplementedException();
    }

    public void CreateSupplier(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public void UpdateSupplier(Supplier supplier)
    {
        throw new NotImplementedException();
    }

    public void DeleteSupplier(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Supplier> GetAllSuppliers()
    {
        throw new NotImplementedException();
    }
}