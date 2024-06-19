using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductService
{
    IQueryable<Product> GetProducts();
    Product GetProductById(Guid id);
    void CreateProduct(Product product);
    void UpdateProduct(Guid id, Product product);
    void DeleteProduct(Guid id);
}