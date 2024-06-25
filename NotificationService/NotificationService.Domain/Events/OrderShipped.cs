namespace Events;

public record OrderShipped
{
    public Guid OrderId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public string ClientEmail { get; init; }
}