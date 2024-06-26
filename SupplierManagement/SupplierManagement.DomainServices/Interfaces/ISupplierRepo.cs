using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierRepo
{
    Task<List<Supplier>> GetAll();
    Task<Supplier?> GetById(Guid id);
    Task Create(Supplier entity);
    Task<Supplier?> Update(Supplier entity);
    Task<Supplier?> Delete(Guid id);
}