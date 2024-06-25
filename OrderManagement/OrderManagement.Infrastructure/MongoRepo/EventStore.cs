using MongoDB.Driver;
using OrderManagement.Domain.Events;
using OrderManagement.DomainServices;

namespace OrderManagement.Infrastructure;

public class EventStore(EventDbContext ctx) : IEventStore
{
    private readonly IMongoCollection<OrderEvent> _orderEvents = ctx.OrderEvents;
    
    public async Task AppendAsync(OrderEvent @event)
    {
        await _orderEvents.InsertOneAsync(@event);
    }

    public async Task<List<OrderEvent>> ReadAsync(Guid id)
    {
        return await _orderEvents
            .Find(e => e.Id == id)
            .SortBy(e => e.CreatedAt)
            .ToListAsync();
    }
}