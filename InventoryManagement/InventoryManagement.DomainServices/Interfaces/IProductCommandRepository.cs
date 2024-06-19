using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductCommandRepository
{
    void Create(Product product);
    void Update(Guid id, Product product);
    void Delete(Guid id);
}