namespace LogisticsManagement.Domain.Events;

public record LogisticsCompanyUpdated : Event
{
    public required Guid LogisticsCompanyId { get; init; }
    public decimal ShippingRate { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}