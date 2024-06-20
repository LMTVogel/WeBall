namespace LogisticsManagement.Domain.Events;

public record LogisticsCompanyCreated : Event
{
    public required Guid LogisticsCompanyId { get; init; }
    public string Name { get; init; }
    public decimal ShippingRate { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}