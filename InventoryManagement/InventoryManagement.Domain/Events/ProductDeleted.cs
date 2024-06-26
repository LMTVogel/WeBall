namespace Events;

public record ProductDeleted()
{
    public Guid Id { get; set; }
}