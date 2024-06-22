namespace PaymentManagement.Domain.Events;

public record OrderCreated
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; }
    public DateTime CreatedAt { get; set; }
}