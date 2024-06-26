using Events;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; }
    public decimal PriceTotal { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string ShippingCompany { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}