using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentManagement.Domain.Entities;

namespace Events;

public record PaymentFailed() : PaymentEvent
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    [BsonRepresentation(BsonType.String)] public PaymentStatus Status { get; init; }
    public override Guid StreamId => PaymentId;
}