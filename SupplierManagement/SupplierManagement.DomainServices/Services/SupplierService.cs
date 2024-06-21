using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Application.Services;

public class SupplierService(IRepo<Supplier> _repo) : ISupplierService
{
    public async Task<IEnumerable<Supplier>> GetAll()
    {
        List<Supplier> suppliers = await _repo.GetAll();
        
        if (suppliers.Count == 0)
            throw new HttpException("No suppliers found", 404);
        
        return suppliers;
    }

    public async Task<Supplier?> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        Supplier? supplier = await _repo.GetById(guid);
        if (supplier == null)
            throw new HttpException("Supplier not found", 404);
        
        return supplier;
    }

    public async Task Create(Supplier supplier)
    {
        await _repo.Create(supplier);
    }

    public async Task Update(string id, Supplier supplier)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        supplier.Id = guid;
        
        if(await _repo.Update(supplier) == null)
            throw new HttpException("Supplier not found", 404);
    }

    public async Task Delete(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        if(await _repo.Delete(guid) == null)
            throw new HttpException("Supplier not found", 404);
    }
}