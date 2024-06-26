using InventoryManagement.Domain.Entities;

namespace InventoryManagement.Domain.Events;

public record ProductCreated()
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal? Price { get; set; }
    public ProductStatus Status { get; set; }
    public Guid SupplierId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}