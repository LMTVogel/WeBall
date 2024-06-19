using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductMongoRepository
{
    void Create(Product product);
    void Update(Guid id, Product product);
    void Delete(Guid id);
    Product GetById(Guid productId);
    IQueryable<Product> GetAll();
}