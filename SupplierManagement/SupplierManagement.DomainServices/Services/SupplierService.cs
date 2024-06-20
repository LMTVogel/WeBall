using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using SupplierManagement.Application.Interfaces;
using SupplierManagement.Domain.Entities;
using SupplierManagement.Domain.Exceptions;

namespace SupplierManagement.Application.Services;

public class SupplierService(IRepo<Supplier> _repo) : ISupplierService
{
    public IEnumerable<Supplier> GetAll()
    {
        if (!_repo.GetAll().Any()) 
            throw new HttpException("No suppliers found", 404);
        
        return _repo.GetAll();
    }

    public Supplier? GetById(Guid id)
    {
        Supplier? supplier = _repo.GetById(id);
        if (supplier == null)
            throw new HttpException("Supplier not found", 404);
        
        return supplier;
    }

    public void Create(Supplier supplier)
    {
        _repo.Create(supplier);
    }

    public void Update(Supplier supplier)
    {
        if(_repo.Update(supplier) == null)
            throw new HttpException("Supplier not found", 404);
    }

    public void Delete(Guid id)
    {
        _repo.Delete(id);
    }
}