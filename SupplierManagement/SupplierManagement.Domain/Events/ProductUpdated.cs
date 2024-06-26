namespace Events;

public class ProductUpdated
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid SupplierId { get; set; }
}