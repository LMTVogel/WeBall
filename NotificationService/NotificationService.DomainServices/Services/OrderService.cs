using MassTransit;
using NotificationService.Application.Interfaces;
using NotificationService.Domain.Entities;
using NotificationService.Domain.Events;

namespace NotificationService.Application.Services;

public class OrderService(IRepository<Order> repo, IPublishEndpoint bus) : IOrderService
{
    public IEnumerable<Order> GetOrders()
    {
        return repo.GetAll();
    }

    public Order GetOrderById(int id)
    {
        return repo.GetById(id);
    }

    public async void CreateOrder(Order order)
    {
        repo.Create(order);

        var message = new OrderCreated()
            { OrderId = order.Id, Name = order.Name, Description = order.Description, Price = order.Price };
        await bus.Publish(message);
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