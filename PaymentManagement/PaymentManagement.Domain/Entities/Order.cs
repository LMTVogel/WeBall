namespace PaymentManagement.Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }
}