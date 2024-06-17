namespace OrderManagement.Domain;

public class Order
{
    public Guid OrderId { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; }
    public decimal TotalAmount => Products.Sum(p => p.TotalPrice);
    public OrderStatus Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string ShippingCompany { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
}
