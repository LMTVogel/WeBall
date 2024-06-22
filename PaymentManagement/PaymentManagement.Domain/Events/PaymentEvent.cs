namespace PaymentManagement.Domain.Events;

public abstract record PaymentEvent
{
    public abstract Guid StreamId { get; }
    public DateTime CreatedAtUtc { get; init; }
};