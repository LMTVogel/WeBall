using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderManagement.Domain.Events;

namespace OrderManagement.Domain;

public class Order
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public DateTime OrderDate { get; set; }
    public List<Product> Products { get; set; }
    public decimal PriceTotal { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string ShippingCompany { get; set; }
    public string ShippingAddress { get; set; }
    public DateTime EstimatedDeliveryDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    private void Apply(OrderCreated @event)
    {
        Id = @event.Id;
        CustomerName = @event.CustomerName;
        CustomerEmail = @event.CustomerEmail;
        OrderDate = @event.OrderDate;
        Products = @event.Products;
        PriceTotal = @event.PriceTotal;
        OrderStatus = @event.OrderStatus;
        PaymentStatus = @event.PaymentStatus;
        ShippingCompany = @event.ShippingCompany;
        ShippingAddress = @event.ShippingAddress;
        EstimatedDeliveryDate = @event.EstimatedDeliveryDate;
        CreatedAt = @event.CreatedAt;
        UpdatedAt = @event.UpdatedAt;
    }
    
    private void Apply(OrderUpdated @event)
    {
        OrderStatus = @event.OrderStatus;
        PaymentStatus = @event.PaymentStatus;
        ShippingCompany = @event.ShippingCompany;
        ShippingAddress = @event.ShippingAddress;
        UpdatedAt = @event.UpdatedAt;
    }
    
    public void Apply(OrderEvent @event)
    {
        switch (@event)
        {
            case OrderCreated created:
                Apply(created);
                break;
            case OrderUpdated updated:
                Apply(updated);
                break;
        }
    }
}
