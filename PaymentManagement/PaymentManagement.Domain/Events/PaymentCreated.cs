using PaymentManagement.Domain.Entities;

namespace PaymentManagement.Domain.Events;

public record PaymentCreated() : PaymentEvent
{
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
    public DateTime PaymentDate { get; init; }
    public string Status { get; init; }
    public Order Order { get; init; }
    public override Guid StreamId => PaymentId;
}