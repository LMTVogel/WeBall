using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsManagement.Domain.Events;

public record LogisticsCompanyDeleted: Event
{
    [BsonRepresentation(BsonType.String)]
    public required Guid LogisticsCompanyId { get; init; }
    public override Guid StreamId => LogisticsCompanyId;
}