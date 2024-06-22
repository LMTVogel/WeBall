using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Application.Services;

public class SupplierService(ISupplierRepo supplierRepo) : ISupplierService
{
    public async Task<IEnumerable<Supplier>> GetAll()
    {
        List<Supplier> suppliers = await supplierRepo.GetAll();
        
        if (suppliers.Count == 0)
            throw new HttpException("No suppliers found", 404);
        
        return suppliers;
    }

    public async Task<Supplier> GetById(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        Supplier? supplier = await supplierRepo.GetById(guid);
        if (supplier == null)
            throw new HttpException("Supplier not found", 404);
        
        return supplier;
    }

    public async Task Create(Supplier supplier)
    {
        try
        {
            await supplierRepo.Create(supplier);
        }
        catch (DbUpdateException ex) when ((ex.InnerException as MySqlException)?.Number == 1062)
        {
            throw new HttpException("Supplier email already exists", 400);
        }
    }

    public async Task Update(string id, Supplier supplier)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        supplier.Id = guid;

        try
        {
            if (await supplierRepo.Update(supplier) == null)
                throw new HttpException("Supplier not found", 404);
        }
        catch (DbUpdateException ex) when ((ex.InnerException as MySqlException)?.Number == 1062)
        {
            throw new HttpException("Supplier email already exists", 400);
        }
    }

    public async Task Delete(string id)
    {
        if(!Guid.TryParse(id, out Guid guid))
            throw new HttpException("Invalid supplier id", 400);
        
        if(await supplierRepo.Delete(guid) == null)
            throw new HttpException("Supplier not found", 404);
    }

    public async Task Verify(string id)
    {
        await Update(id, new Supplier { status = true });
    }
}