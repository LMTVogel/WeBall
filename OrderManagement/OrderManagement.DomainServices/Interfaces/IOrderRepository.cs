using OrderManagement.Domain;

namespace OrderManagement.DomainServices;

public interface IOrderRepository
{
    Task<Order> GetOrderById(Guid orderId);
    Task<IQueryable<Order>> GetAllOrders();
    Task CreateOrder(Order order);
    Task UpdateOrder(Order order);
    Task<IQueryable<Order>> GetOrderHistory(Guid orderId);
}