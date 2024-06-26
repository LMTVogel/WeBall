namespace Events;
public abstract record OrderEvent
{
    public abstract Guid StreamId { get; }
    public DateTime CreatedAtUtc { get; init; }
};