using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentManagement.Domain.Entities;

namespace Events;

public record PaymentPaid : PaymentEvent
{
    public Guid PaymentId { get; init; }
    public Guid OrderId { get; init; }
    public string CustomerEmail { get; init; }

    [BsonRepresentation(BsonType.String)] public PaymentStatus Status { get; init; }
    public override Guid StreamId => PaymentId;
}