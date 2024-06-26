namespace PaymentManagement.Domain.Entities;
//
// public class Order
// {
//     public Guid Id { get; set; }
//     public Guid CustomerId { get; set; }
//     public List<Product> Products { get; set; }
//     public PaymentMethod PaymentMethod { get; set; }
// }

public class Order
{
    public Guid Id { get; init; }

    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }

    public DateTime OrderDate { get; init; }

    public List<Product> Products { get; init; }
    public decimal PriceTotal { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
    public PaymentMethod PaymentMethod { get; set; }
    public string ShippingCompany { get; init; }
    public string ShippingAddress { get; init; }

    public DateTime EstimatedDeliveryDate { get; init; }

    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; init; }
}

public enum OrderStatus
{
    Pending,
    Processing,
    Shipped,
    Delivered,
    Cancelled
}