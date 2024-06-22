namespace PaymentManagement.Domain.Events;

public record PaymentCancelled() : PaymentEvent
{
    public string Status { get; init; }
    public Guid PaymentId { get; init; }
    public override Guid StreamId => PaymentId;
}