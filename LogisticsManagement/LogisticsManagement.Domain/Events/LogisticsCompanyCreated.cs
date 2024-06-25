using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Events;

public record LogisticsCompanyCreated : LogisticsCompanyEvent
{
    [BsonRepresentation(BsonType.String)]
    public required Guid LogisticsCompanyId { get; init; }
    public string? Name { get; init; }
    public decimal ShippingRate { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}