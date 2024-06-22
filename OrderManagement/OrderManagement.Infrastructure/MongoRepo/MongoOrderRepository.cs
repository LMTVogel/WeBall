using MongoDB.Driver;
using OrderManagement.Domain;
using OrderManagement.DomainServices;

namespace OrderManagement.Infrastructure;

public class MongoOrderRepository(MongoDbContext dbContext) : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders = dbContext.Orders;

    public async Task<Order> GetOrderById(Guid orderId)
    {
        return await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
    }

    public async Task<IQueryable<Order>> GetAllOrders()
    {
        var result = _orders.AsQueryable();
        return await Task.FromResult(result);
    }

    public async Task CreateOrder(Order order)
    {
        await _orders.InsertOneAsync(order);
    }

    public async Task UpdateOrder(Order order)
    {
        await _orders.ReplaceOneAsync(o => o.Id == order.Id, order);
    }

    public async Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
    {
        var result = _orders.AsQueryable().Where(o => o.Id == orderId);
        return await Task.FromResult(result);
    }
}