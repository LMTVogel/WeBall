using InventoryManagement.Domain.Entities;

namespace InventoryManagement.DomainServices.Interfaces;

public interface IProductQueryRepository
{
    Product GetById(Guid productId);
    IQueryable<Product> GetAll();
}