using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using OrderManagement.Domain;

namespace Events;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(OrderCreated), typeof(OrderUpdated))]
public abstract record OrderEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; init; }
    [BsonRepresentation(BsonType.String)]
    public Guid OrderId { get; init; }
    
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