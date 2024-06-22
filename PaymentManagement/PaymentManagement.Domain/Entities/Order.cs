namespace PaymentManagement.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public List<Product> Products { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}