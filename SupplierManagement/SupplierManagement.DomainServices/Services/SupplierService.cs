using Microsoft.EntityFrameworkCore;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Application.Services;

public class SupplierService(IRepo<Supplier> _repo) : ISupplierService
{
    public IEnumerable<Supplier> GetAll()
    {
        if (_repo.GetAll().Any()) 
            throw new HttpException("No suppliers found", 404);
        
        return _repo.GetAll();
    }

    public Supplier? GetById(string id)
    {
        Supplier? supplier = _repo.GetById(id);
        if (supplier == null)
            throw new HttpException("Supplier not found", 404);
        
        return supplier;
    }

    public void Create(Supplier supplier)
    {
        try
        {
            _repo.Create(supplier);
        }
        catch(DbUpdateException ex)
        {
            
        }
    }

    public void Update(string id, Supplier supplier)
    {
        // _repo.Update(supplier);
        throw new HttpException("Supplier not found", 404);
    }

    public void Delete(string id)
    {
        _repo.Delete(id);
    }
}