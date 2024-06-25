using PaymentManagement.Domain.Events;

namespace PaymentManagement.DomainServices.Interfaces;

public interface IEventStore<in TEvent> where TEvent : class
{
    /// <summary>
    /// Add event to the event store
    /// </summary>
    /// <param name="event">event</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task AppendAsync<T>(T @event) where T : TEvent;

    /// <summary>
    /// Get a list of events from the event store
    /// </summary>
    /// <param name="streamId"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<List<T>> ReadAsync<T>(Guid streamId) where T : TEvent;
}