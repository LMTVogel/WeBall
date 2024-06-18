namespace NotificationService.Domain.Events;

public record OrderCreated
{
    public int OrderId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}