using LogisticsManagement.Domain.Events;
using LogisticsManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

/// <summary>
/// event store for the logistics company
/// </summary>
public class LcEventStore(EventDbContext ctx) : IEventStore
{
    public async Task AppendAsync<T>(T @event) where T : Event
    {
        var eventCollection = ctx.Events.OfType<T>();
        await eventCollection.InsertOneAsync(@event);
    }

    public async Task<List<T>> ReadAsync<T>(Guid streamId) where T : Event
    {
        var eventCollection = ctx.Events.OfType<T>();
        var projection = Builders<T>.Projection.Exclude("_id");

        return await eventCollection
            .Find(x => x.StreamId == streamId)
            .Project<T>(projection)
            .SortBy(x => x.CreatedAtUtc).ToListAsync();
    }
}