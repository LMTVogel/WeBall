using System.Text.Json.Serialization;
using LogisticsManagement.Domain.Entities;

namespace LogisticsManagement.Domain.Events;

[JsonPolymorphic]
[JsonDerivedType(typeof(LogisticsCompanyCreated), nameof(LogisticsCompanyCreated))]
[JsonDerivedType(typeof(LogisticsCompanyUpdated), nameof(LogisticsCompanyUpdated))]
[JsonDerivedType(typeof(LogisticsCompanyDeleted), nameof(LogisticsCompanyDeleted))]
public abstract record Event
{
    public abstract Guid StreamId { get; }
    public DateTime CreatedAtUtc { get; set; }
}