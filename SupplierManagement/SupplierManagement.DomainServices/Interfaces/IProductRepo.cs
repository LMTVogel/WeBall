using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IProductRepo
{
    Task<List<Product>> GetAllBySupplier(Guid supplierId);
    Task<Product?> GetById(Guid id);
    Task Create(Product entity);
    Task<Product?> Update(Product entity);
    Task<Product?> Delete(Guid id);
}