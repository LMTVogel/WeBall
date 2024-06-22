using PaymentManagement.Domain.Entities;

namespace PaymentManagement.Domain.Events;

public abstract record OrderCreated
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public List<Product> Products { get; set; }
    public DateTime CreatedAt { get; set; }
}