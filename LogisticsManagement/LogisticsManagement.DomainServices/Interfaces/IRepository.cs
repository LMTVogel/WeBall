using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.DomainServices.Interfaces;

public interface IRepository<T> where T : class
{
    Task<LogisticsCompany?> GetByIdAsync(Guid id);
    Task<List<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(Guid id, T entity);
    Task DeleteAsync(Guid id);
}