using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OrderManagement.Domain.Events;

public record OrderUpdated : OrderEvent
{
    public OrderUpdated()
    {
        UpdatedAt = DateTime.Now;
    }
}