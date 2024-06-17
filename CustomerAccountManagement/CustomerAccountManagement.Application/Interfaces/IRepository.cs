using System.Collections.Generic;

namespace CustomerAccountManagement.Application.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        void Create(T entity);
        void Update(int id, T entity);
        void Delete(int id);
    }
}