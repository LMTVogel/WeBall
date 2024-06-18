using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces;

public interface IOrderService
{
    IEnumerable<Order> GetOrders();
    Order GetOrderById(Guid id);
    void CreateOrder(Order order);
    void UpdateOrder(Guid id, Order order);
    void DeleteOrder(Guid id);
}