using LogisticsManagement.Domain.Events;

namespace LogisticsManagement.DomainServices.Interfaces;

/// <summary>
/// Event store
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// Add event to the event store
    /// </summary>
    /// <param name="event">event</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task AppendAsync<T>(T @event) where T : Event;

    /// <summary>
    /// Get a list of events from the event store
    /// </summary>
    /// <param name="streamId"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<List<T>> ReadAsync<T>(Guid streamId) where T : Event;
}