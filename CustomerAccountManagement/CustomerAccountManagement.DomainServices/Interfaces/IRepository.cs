using System.Collections.Generic;
using CustomerAccountManagement.Domain.Entities;

namespace CustomerAccountManagement.DomainServices.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        void Create(T entity);
        Customer Update(Guid id, T entity);
        void Delete(T entity);
    }
}