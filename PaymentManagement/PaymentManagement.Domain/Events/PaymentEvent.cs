using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PaymentManagement.Domain.Events;

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(PaymentCreated), typeof(PaymentPaid), typeof(PaymentCancelled))]
public abstract record PaymentEvent
{
    [BsonRepresentation(BsonType.String)] public abstract Guid StreamId { get; }

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAtUtc { get; init; }
};