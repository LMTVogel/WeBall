using MongoDB.Driver;
using OrderManagement.Domain;
using OrderManagement.DomainServices;

namespace OrderManagement.Infrastructure;

public class MongoOrderRepository : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders;
    
    public MongoOrderRepository(MongoDbContext dbContext)
    {
        _orders = dbContext.Orders;
    }
    
    public async Task<Order> GetOrderById(Guid orderId)
    {
        return await _orders.Find(o => o.Id == orderId).FirstOrDefaultAsync();
    }

    public Task<IQueryable<Order>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public Task CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task UpdateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
    {
        throw new NotImplementedException();
    }
}