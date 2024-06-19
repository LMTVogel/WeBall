using InventoryManagement.Domain.Entities;
using InventoryManagement.DomainServices.Interfaces;

namespace InventoryManagement.Infrastructure.SqlRepo;

public class ProductSqlRepository(SqlDbContext context) : IProductCommandRepository
{
    public void Create(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public void Update(Guid id, Product product)
    {
        context.Products.Update(product);
        context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        //TODO: Dit moet naar mongo worden overgezet???
        var product = context.Products.Find(id);
        context.Products.Remove(product);
        context.SaveChanges();
    }
}