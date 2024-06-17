using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierService
{
    IEnumerable<Supplier> GetSuppliers();
    Supplier GetSupplierById(int id);
    void CreateSupplier(Supplier supplier);
    void UpdateSupplier(Supplier supplier);
    void DeleteSupplier(int id);
}