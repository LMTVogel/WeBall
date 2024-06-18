namespace NotificationService.Application.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(Guid id);
    void Create(T entity);
    void Update(Guid id, T entity);
    void Delete(Guid id);
}