namespace PaymentManagement.Domain.Events;

public record PaymentPaid : PaymentEvent
{
    public Guid PaymentId { get; init; }
    public decimal Amount { get; init; }
    public DateTime PaymentDate { get; init; }
    public string Status { get; init; }
    public override Guid StreamId => PaymentId;
}