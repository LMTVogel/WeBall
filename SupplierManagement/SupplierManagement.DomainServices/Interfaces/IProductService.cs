using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllBySupplier(string supplierId);
    Task<Product?> GetById(string id);
    Task Create(string supplierId, Product supplier);
    Task Update(string id, Product product);
    Task Delete(string id);
}