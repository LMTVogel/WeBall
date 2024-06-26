namespace Events;

public record LogisticsCompanyUpdated : LogisticsCompanyEvent
{
    public required Guid LogisticsCompanyId { get; init; }
    public decimal ShippingRate { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}