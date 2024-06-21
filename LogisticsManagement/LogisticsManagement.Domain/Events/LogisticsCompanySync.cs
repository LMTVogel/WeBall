namespace LogisticsManagement.Domain.Events;


/// <summary>
/// Event to sync the logistics company
/// the event is raised after event sourcing a projection of the current logistics company
/// </summary>
public record LogisticsCompanySync : Event
{
    public Guid LogisticsCompanyId { get; init; }
    public string Name { get; init; }
    public decimal ShippingRate { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}