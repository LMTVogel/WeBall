using OrderManagement.Domain;

namespace OrderManagement.DomainServices;

public interface IOrderService
{
    Task<Order> GetOrderById(Guid orderId);
    Task<IQueryable<Order>> GetAllOrders();
    Task CreateOrder(Order order);
    Task UpdateOrder(Guid id, Order order);
    Task<IQueryable<Order>> GetOrderHistory(Guid orderId);
}