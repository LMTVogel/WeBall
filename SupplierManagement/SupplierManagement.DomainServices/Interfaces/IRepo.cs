using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IRepo<T> where T : class
{
    IQueryable<T> GetAll();
    T? GetById(Guid id);
    void Create(T supplier);
    Supplier? Update(T supplier);
    void Delete(Guid id);
}