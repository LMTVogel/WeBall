using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Events;

public record ProductDeleted()
{
    public Guid Id { get; set; }
}