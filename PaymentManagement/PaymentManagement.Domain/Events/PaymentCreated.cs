using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentManagement.Domain.Entities;

namespace PaymentManagement.Domain.Events;

public record PaymentCreated() : PaymentEvent
{
    public Guid PaymentId { get; init; }

    [BsonRepresentation(BsonType.String)] public PaymentStatus Status { get; init; }
    public decimal Amount { get; init; }
    public Order Order { get; init; }
    public override Guid StreamId => PaymentId;
}