using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProducts();
    Task<Product?> GetProductById(Guid id);
    Task CreateProduct(Product product);
    Task UpdateProduct(Guid id, Product product);
    Task DeleteProduct(Guid id);
}