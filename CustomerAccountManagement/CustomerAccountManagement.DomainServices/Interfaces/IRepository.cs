using System.Collections.Generic;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T?> GetById(Guid id);
        Task Create(T entity);
        Task<T?> Update(Guid id, T entity);
        Task<T?> Delete(T entity);
    }
}