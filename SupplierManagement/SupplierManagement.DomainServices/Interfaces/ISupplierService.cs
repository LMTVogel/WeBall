using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierService
{
    IEnumerable<Supplier> GetAll();
    Supplier? GetById(Guid id);
    void Create(Supplier supplier);
    void Update(Supplier supplier);
    void Delete(Guid id);
}