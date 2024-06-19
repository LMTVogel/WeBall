using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierService
{
    IEnumerable<Supplier> GetAll();
    Supplier? GetById(string id);
    void Create(Supplier supplier);
    void Update(string id, Supplier supplier);
    void Delete(string id);
}