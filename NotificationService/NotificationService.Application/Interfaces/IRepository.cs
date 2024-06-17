namespace NotificationService.Application.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Create(T entity);
    void Update(int id, T entity);
    void Delete(int id);
}