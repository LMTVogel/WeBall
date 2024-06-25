namespace SupplierManagement.Domain.Events;

public class ProductCreated
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
}