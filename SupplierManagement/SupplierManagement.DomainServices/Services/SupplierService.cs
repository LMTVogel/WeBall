using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Services;

public class SupplierService(IRepo<Supplier> _repo) : ISupplierService
{
    public IEnumerable<Supplier> GetAll()
    {
        return _repo.GetAll();
    }

    public Supplier GetById(int id)
    {
        return _repo.GetById(id);
    }

    public void Create(Supplier supplier)
    {
        _repo.Create(supplier);
    }

    public void Update(Supplier supplier)
    {
        _repo.Update(supplier);
    }

    public void Delete(int id)
    {
        _repo.Delete(id);
    }
}