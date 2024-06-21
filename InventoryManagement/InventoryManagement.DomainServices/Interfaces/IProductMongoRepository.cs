using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductMongoRepository
{
    Task Create(Product product);
    Task<Product?> Update(Product product);
    Task<Product?> Delete(Guid id);
    Task<Product?> GetById(Guid productId);
    Task<List<Product>> GetAll();
}