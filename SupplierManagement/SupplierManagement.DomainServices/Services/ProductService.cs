using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Application.Services;

public class ProductService(IProductRepo productRepo, ISupplierService _supplierService) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllBySupplier(string supplierId)
    {
        if(!Guid.TryParse(supplierId, out Guid supplierGuidId))
            throw new HttpException("Invalid supplier id", 400);
        
        List<Product> products = await productRepo.GetAllBySupplier(supplierGuidId);
    
        if (products.Count == 0)
            throw new HttpException("No products found", 404);
    
        return products;
    }

    public async Task<Product?> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid product id", 400);
        
        Product? product = await productRepo.GetById(guid);
        if (product == null)
            throw new HttpException("Product not found", 404);
        
        return product;
    }

    public async Task Create(string supplierId, Product product)
    {
        Supplier supplier = await _supplierService.GetById(supplierId);
        product.Supplier = supplier;
        await productRepo.Create(product);
    }

    public async Task Update(string id, Product product)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid product id", 400);
        
        product.Id = guid;
        
        if(await productRepo.Update(product) == null)
            throw new HttpException("Product not found", 404);
    }

    public async Task Delete(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid product id", 400);
        
        if(await productRepo.Delete(guid) == null)
            throw new HttpException("Product not found", 404);
    }
}