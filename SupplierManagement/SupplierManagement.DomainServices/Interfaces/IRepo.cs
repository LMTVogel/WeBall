using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IRepo<T> where T : class
{
    IQueryable<T> GetAll();
    T GetById(int id);
    void Create(T supplier);
    void Update(T supplier);
    void Delete(int id);
}