namespace Events;

public record OrderCreated
{
    public Guid OrderId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    
    public string CustomerEmail { get; init; }
}