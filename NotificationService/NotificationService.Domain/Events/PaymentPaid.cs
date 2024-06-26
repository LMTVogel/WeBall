namespace Events;

public record PaymentPaid()
{
    public Guid OrderId { get; init; }
    public string CustomerEmail { get; init; }
}