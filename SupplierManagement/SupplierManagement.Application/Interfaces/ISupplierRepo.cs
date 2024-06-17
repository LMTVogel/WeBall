using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierRepo
{
    IQueryable<Supplier> GetAll();
    IQueryable<Supplier> GetSuppliers();
    Supplier GetSupplierById(int id);
    void CreateSupplier(Supplier supplier);
    void UpdateSupplier(Supplier supplier);
    void DeleteSupplier(int id);
    IQueryable<Supplier> GetAllSuppliers();
}