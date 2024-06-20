using LogisticsManagement.Domain.Entities;
using LogisticsManagement.Domain.Events;
using LogisticsManagement.DomainServices.Interfaces;
using MongoDB.Driver;

namespace LogisticsManagement.Infrastructure.Repositories;

/// <summary>
/// event store for the logistics company
/// </summary>
public class LcEventStore(MongoDbContext ctx) : IEventStore
{
    public async Task AppendAsync<T>(T @event) where T : Event
    {
        await ctx.Events.InsertOneAsync(@event);
    }

    public async Task<List<Event>> ReadAsync<T>(Guid streamId) where T : Event
    {
        // return all events for the streamId
        // sorted by the event timestamp
        return await ctx.Events
            .Find(x => x.StreamId == streamId)
            .SortBy(x => x.CreatedAtUtc).ToListAsync();
    }
}