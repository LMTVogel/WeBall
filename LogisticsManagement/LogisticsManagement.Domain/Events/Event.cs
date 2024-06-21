using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LogisticsManagement.Domain.Events;

// [JsonPolymorphic]
// [JsonDerivedType(typeof(LogisticsCompanyCreated), nameof(LogisticsCompanyCreated))]
// [JsonDerivedType(typeof(LogisticsCompanyUpdated), nameof(LogisticsCompanyUpdated))]
// [JsonDerivedType(typeof(LogisticsCompanyDeleted), nameof(LogisticsCompanyDeleted))]
//

[BsonDiscriminator(RootClass = true)]
[BsonKnownTypes(typeof(LogisticsCompanyCreated), typeof(LogisticsCompanyUpdated), typeof(LogisticsCompanyDeleted))]
public abstract record Event
{
    [BsonRepresentation(BsonType.String)]
    public abstract Guid StreamId { get; }
    
    [BsonRepresentation(BsonType.DateTime)]
    public DateTime CreatedAtUtc { get; init; }
}