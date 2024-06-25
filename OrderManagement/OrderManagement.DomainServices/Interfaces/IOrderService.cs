using OrderManagement.Domain;

namespace OrderManagement.DomainServices;

public interface IOrderService
{
    Task<Order> GetOrderById(Guid orderId);
    Task<IQueryable<Order>> GetAllOrders();
    Task CreateOrderAsync(Order order);
    Task UpdateOrderAsync(Guid id, Order order);
    Task<IQueryable<Order>> GetOrderHistory(Guid orderId);
}