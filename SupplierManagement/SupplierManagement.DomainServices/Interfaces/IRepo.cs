using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface IRepo<T> where T : class
{
    Task<List<Supplier>> GetAll();
    Task<T?> GetById(Guid id);
    Task Create(T supplier);
    Task<Supplier?> Update(T supplier);
    Task<Supplier?> Delete(Guid id);
}