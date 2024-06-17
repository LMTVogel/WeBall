using NotificationService.Domain.Entities;

namespace NotificationService.Application.Interfaces;

public interface IOrderService
{
    IEnumerable<Order> GetOrders();
    Order GetOrderById(int id);
    void CreateOrder(Order order);
    void UpdateOrder(int id, Order order);
    void DeleteOrder(int id);
}