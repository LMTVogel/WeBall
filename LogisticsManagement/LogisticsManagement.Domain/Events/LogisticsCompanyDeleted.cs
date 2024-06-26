using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Events;

public record LogisticsCompanyDeleted: LogisticsCompanyEvent
{
    [BsonRepresentation(BsonType.String)]
    public required Guid LogisticsCompanyId { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}