using Events;
using MongoDB.Driver;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.Infrastructure.Repositories;

public class PaymentEventStore(EventDbContext ctx): IEventStore<PaymentEvent>
{
    public async Task AppendAsync<T>(T @event) where T : PaymentEvent 
    {
        var eventCollection = ctx.PaymentEvents.OfType<T>();
        await eventCollection.InsertOneAsync(@event);
    }

    public async Task<List<T>> ReadAsync<T>(Guid streamId) where T : PaymentEvent 
    {
        var projection = Builders<PaymentEvent>.Projection.Exclude("_id");

        return await ctx.PaymentEvents
            .Find(e => e.StreamId == streamId)
            .Project<T>(projection)
            .SortBy(e => e.CreatedAtUtc)
            .ToListAsync();
    }
}