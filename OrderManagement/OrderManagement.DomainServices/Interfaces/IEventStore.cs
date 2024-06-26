using Events;

namespace OrderManagement.DomainServices;

/// <summary>
/// Event store
/// </summary>
public interface IEventStore
{
    /// <summary>
    /// Add event to the event store
    /// </summary>
    Task AppendAsync(OrderEvent orderEvent);
    
    /// <summary>
    /// Get a list of order events from the event store
    /// </summary>
    /// <returns></returns>
    Task<List<OrderEvent>> ReadAsync(Guid id);
}