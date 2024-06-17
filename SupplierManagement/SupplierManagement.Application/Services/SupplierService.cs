using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Services;

public class SupplierService : ISupplierService
{
    private readonly ISupplierRepo _supplierRepo;
    
    public IEnumerable<Supplier> GetSuppliers()
    {
        return _supplierRepo.GetSuppliers();
    }
    
    public Supplier GetSupplierById(int id)
    {
        return _supplierRepo.GetSupplierById(id);
    }
    
    public void CreateSupplier(Supplier supplier)
    {
        _supplierRepo.CreateSupplier(supplier);
    }
    
    public void UpdateSupplier(Supplier supplier)
    {
        _supplierRepo.UpdateSupplier(supplier);
    }
    
    public void DeleteSupplier(int id)
    {
        _supplierRepo.DeleteSupplier(id);
    }
}