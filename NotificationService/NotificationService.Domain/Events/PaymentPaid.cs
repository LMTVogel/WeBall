namespace Events;

public record PaymentPaid()
{
    public Guid OrderId { get; init; }
    public string ClientEmail { get; init; }
}