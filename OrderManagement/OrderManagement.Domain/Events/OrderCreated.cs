using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderManagement.Domain.Events;

public record OrderCreated : OrderEvent
{
    public OrderCreated()
    {
        CreatedAt = DateTime.Now;
    }
}