using System.Collections.Generic;

namespace CustomerAccountManagement.DomainServices.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        void Create(T entity);
        void Update(Guid id, T entity);
        void Delete(T entity);
    }
}