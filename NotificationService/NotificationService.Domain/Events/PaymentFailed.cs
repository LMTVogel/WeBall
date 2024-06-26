namespace Events;

public record PaymentFailed()
{
    public Guid OrderId { get; init; }
    public string CustomerEmail { get; init; }
}