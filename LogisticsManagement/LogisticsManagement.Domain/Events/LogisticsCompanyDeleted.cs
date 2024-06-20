namespace LogisticsManagement.Domain.Events;

public record LogisticsCompanyDeleted: Event
{
    public required Guid LogisticsCompanyId { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}