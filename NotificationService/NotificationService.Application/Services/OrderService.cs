using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;

namespace NotificationService.Application.Services;

public class OrderService(IRepository<Order> repo, IMessageProducer producer) : IOrderService
{
    public IEnumerable<Order> GetOrders()
    {
        return repo.GetAll();
    }

    public Order GetOrderById(int id)
    {
        return repo.GetById(id);
    }

    public void CreateOrder(Order order)
    {
        Console.WriteLine("Create order");
        producer.SendMessage("CreateOrder", order);
        repo.Create(order);
    }

    public void UpdateOrder(int id, Order order)
    {
        repo.Update(id, order);
    }

    public void DeleteOrder(int id)
    {
        repo.Delete(id);
    }
}