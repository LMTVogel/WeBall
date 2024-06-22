using PaymentManagement.Domain.Entities;

namespace PaymentManagement.Domain.Events;

public record PaymentPaid : PaymentEvent
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public PaymentStatus Status { get; init; }
    public override Guid StreamId => PaymentId;
}