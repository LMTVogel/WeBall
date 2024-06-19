using OrderManagement.Domain;

namespace OrderManagement.DomainServices;

public class OrderService(IOrderRepository orderRepository) : IOrderService
{
    public Task<Order> GetOrderById(Guid orderId)
    {
        return orderRepository.GetOrderById(orderId);
    }

    public Task<IQueryable<Order>> GetAllOrders()
    {
        return orderRepository.GetAllOrders();
    }

    public Task CreateOrder(Order order)
    {
        return orderRepository.CreateOrder(order);
    }

    public Task UpdateOrder(Order order)
    {
        return orderRepository.UpdateOrder(order);
    }

    public Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
    {
        return orderRepository.GetOrderHistory(orderId);
    }
}