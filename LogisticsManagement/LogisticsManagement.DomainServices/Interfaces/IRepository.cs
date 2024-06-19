namespace LogisticsManagement.DomainServices.Interfaces;

public interface IRepository<T> where T : class
{
    T? GetById(Guid id);
    IQueryable<T> GetAll();
    void Add(T entity);
    void Update(Guid id, T entity);
    void Delete(Guid id);
}