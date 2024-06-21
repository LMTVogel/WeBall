using SupplierManagement.Domain.Entities;

namespace SupplierManagement.Application.Interfaces;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetAll();
    Task<Supplier?> GetById(string id);
    Task Create(Supplier supplier);
    Task Update(string id, Supplier supplier);
    Task Delete(string id);
}