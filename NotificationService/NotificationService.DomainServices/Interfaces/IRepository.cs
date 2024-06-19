namespace NotificationService.Application.Interfaces;

public interface IRepository<T> where T : class
{
    T GetById(Guid id);
    IQueryable<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}