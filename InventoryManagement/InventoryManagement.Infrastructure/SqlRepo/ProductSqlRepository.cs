using InventoryManagement.Domain.Entities;
using InventoryManagement.DomainServices.Interfaces;

namespace InventoryManagement.Infrastructure.SqlRepo;

public class ProductSqlRepository(SqlDbContext context) : IProductCommandRepository
{
    public async Task<Product> Create(Product product)
    {
        var p = context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return p.Result.Entity;
    }

    public async Task<Product?> Update(Product product)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync();
        return product;
    }

    public async Task<Product?> Delete(Product product)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync();
        return product;
    }
    
    public async Task<Product?> GetById(Guid productId)
    {
        return await context.Products.FindAsync(productId);
    }
}