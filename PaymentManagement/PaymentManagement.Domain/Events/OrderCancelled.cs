namespace PaymentManagement.Domain.Events;

public record OrderCancelled : OrderEvent
{
    public Guid OrderId { get; init; }
    public override Guid StreamId => OrderId;
}