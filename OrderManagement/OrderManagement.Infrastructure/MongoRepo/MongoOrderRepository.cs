using MongoDB.Driver;
using OrderManagement.Domain;
using OrderManagement.DomainServices;

namespace OrderManagement.Infrastructure;

public class MongoOrderRepository(MongoDbContext dbContext) : IOrderRepository
{
    private readonly IMongoCollection<Order> _orders = dbContext.Orders;

    public async Task<Order> GetOrderById(Guid orderId)
    {
        return await _orders.Find(o => o.OrderId == orderId).FirstOrDefaultAsync();
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
        var filter = Builders<Order>.Filter.Eq(o => o.OrderId, order.OrderId);
        var update = Builders<Order>.Update
            .Set(o => o.CustomerName, order.CustomerName)
            .Set(o => o.CustomerEmail, order.CustomerEmail)
            .Set(o => o.OrderDate, order.OrderDate)
            .Set(o => o.Products, order.Products)
            .Set(o => o.PriceTotal, order.PriceTotal)
            .Set(o => o.OrderStatus, order.OrderStatus)
            .Set(o => o.PaymentStatus, order.PaymentStatus)
            .Set(o => o.ShippingCompany, order.ShippingCompany)
            .Set(o => o.ShippingAddress, order.ShippingAddress)
            .Set(o => o.EstimatedDeliveryDate, order.EstimatedDeliveryDate)
            .Set(o => o.UpdatedAt, order.UpdatedAt);
            
        await _orders.UpdateOneAsync(filter, update);
    }

    public async Task<IQueryable<Order>> GetOrderHistory(Guid orderId)
    {
        var result = _orders.AsQueryable().Where(o => o.OrderId == orderId);
        return await Task.FromResult(result);
    }
}