using System.Collections;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly ICollection<Order> _orders = new List<Order>();

    public IEnumerable<Order> GetAll()
    {
        return _orders;
    }

    public Order GetById(Guid id)
    {
        return _orders.First(o => o.Id == id);
    }

    public void Create(Order order)
    {
        _orders.Add(order);
    }

    public void Update(Guid id, Order order)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}