using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductCommandRepository
{
    Task<Product> Create(Product product);
    Task<Product?> Update(Product product);
    Task<Product?> Delete(Guid id);
}