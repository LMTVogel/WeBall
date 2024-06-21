using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.DomainServices.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task CreateAsync(T entity);
    Task<T?> UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}