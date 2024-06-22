using PaymentManagement.Domain.Entities;

namespace PaymentManagement.Domain.Events;

public record PaymentCancelled() : PaymentEvent
{
    public PaymentStatus Status { get; init; }
    public Guid PaymentId { get; init; }
    public override Guid StreamId => PaymentId;
}