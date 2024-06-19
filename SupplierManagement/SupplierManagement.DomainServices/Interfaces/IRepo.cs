using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IRepo<T> where T : class
{
    IQueryable<T> GetAll();
    T? GetById(string id);
    void Create(T supplier);
    void Update(string id, T supplier);
    void Delete(string id);
}