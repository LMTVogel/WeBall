namespace NotificationService.Domain.Events;

public record OrderPayed
{
    public int OrderId { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
}