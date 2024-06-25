namespace Events;

public record PaymentCancelled()
{
    public Guid OrderId { get; init; }
}