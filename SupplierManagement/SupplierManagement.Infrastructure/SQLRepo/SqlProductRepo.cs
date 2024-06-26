using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Infrastructure.SQLRepo;

public class SqlProductRepo(SQLDbContext _context) : IProductRepo
{
    public async Task<List<Product>> GetAllBySupplier(Guid supplierId)
    {
        return await _context.Products.Where(p => p.SupplierId == supplierId).ToListAsync();
    }

    public async Task<Product?> GetById(Guid id)
    {
        return await _context.Products.FindAsync(id);
    }

    public async Task Create(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> Update(Product products)
    {
        var existingProduct = await GetById(products.Id);

        if (existingProduct == null)
            return null;

        existingProduct.Name = products.Name;

        _context.Products.Update(existingProduct);
        await _context.SaveChangesAsync();

        return existingProduct;
    }

    public async Task<Product?> Delete(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null) 
            return null;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return product;
    }
}