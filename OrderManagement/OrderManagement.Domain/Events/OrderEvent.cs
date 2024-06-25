using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderManagement.Domain.Events;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(OrderCreated), typeof(OrderUpdated))]
public abstract record OrderEvent
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; init; } // De unieke identifier voor de order

    public string CustomerName { get; init; }
    public string CustomerEmail { get; init; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime OrderDate { get; init; }
    
    public List<Product> Products { get; init; }
    public decimal PriceTotal { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public PaymentStatus PaymentStatus { get; init; }
    public string ShippingCompany { get; init; }
    public string ShippingAddress { get; init; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime EstimatedDeliveryDate { get; init; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAt { get; init; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime UpdatedAt { get; init; }
}