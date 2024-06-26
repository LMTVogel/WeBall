namespace Events;

public record OrderShipped()
{
    public Guid OrderId { get; init; }
    public string CustomerEmail { get; init; }
}