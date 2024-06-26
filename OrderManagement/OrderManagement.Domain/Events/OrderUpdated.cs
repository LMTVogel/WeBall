using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Events;

public record OrderUpdated : OrderEvent
{
}