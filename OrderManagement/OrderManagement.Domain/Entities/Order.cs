using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Events;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Domain;

public class Order
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonRepresentation(BsonType.String)] public Guid OrderId { get; set; }
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

    private void Apply(OrderCreated @event)
    {
        OrderId = @event.OrderId;
        CustomerName = @event.CustomerName;
        CustomerEmail = @event.CustomerEmail;
        OrderDate = @event.OrderDate;
        Products = @event.Products;
        PriceTotal = @event.PriceTotal;
        OrderStatus = @event.OrderStatus;
        PaymentStatus = @event.PaymentStatus;
        PaymentMethod = @event.PaymentMethod;
        ShippingCompany = @event.ShippingCompany;
        ShippingAddress = @event.ShippingAddress;
        EstimatedDeliveryDate = @event.EstimatedDeliveryDate;
        CreatedAt = @event.CreatedAt;
        UpdatedAt = @event.UpdatedAt;
    }

    private void Apply(OrderUpdated @event)
    {
        OrderId = @event.OrderId;
        CustomerName = @event.CustomerName;
        CustomerEmail = @event.CustomerEmail;
        OrderDate = @event.OrderDate;
        Products = @event.Products;
        PriceTotal = @event.PriceTotal;
        OrderStatus = @event.OrderStatus;
        PaymentStatus = @event.PaymentStatus;
        PaymentMethod = @event.PaymentMethod;
        ShippingCompany = @event.ShippingCompany;
        ShippingAddress = @event.ShippingAddress;
        EstimatedDeliveryDate = @event.EstimatedDeliveryDate;
        CreatedAt = @event.CreatedAt;
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