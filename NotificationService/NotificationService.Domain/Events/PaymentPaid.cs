namespace Events;

public record PaymentPaid()
{
    public Guid OrderId { get; init; }
}