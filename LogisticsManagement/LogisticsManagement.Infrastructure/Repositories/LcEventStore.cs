using Events;
using LogisticsManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

/// <summary>
/// event store for the logistics company
/// </summary>
public class LcEventStore(EventDbContext ctx) : IEventStore
{
    public async Task AppendAsync<T>(T @event) where T : LogisticsCompanyEvent
    {
        var eventCollection = ctx.Events.OfType<T>();
        await eventCollection.InsertOneAsync(@event);
    }

    public async Task<List<T>> ReadAsync<T>(Guid streamId) where T : LogisticsCompanyEvent
    {
        var projection = Builders<LogisticsCompanyEvent>.Projection.Exclude("_id");

        return await ctx.Events
            .Find(e => e.StreamId == streamId)
            .Project<T>(projection)
            .SortBy(e => e.CreatedAtUtc)
            .ToListAsync();
    }
}