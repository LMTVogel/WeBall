using PaymentManagement.Domain.Entities;

namespace Events;

public record OrderUpdated : OrderEvent
{
    public Guid OrderId { get; init; }
    public OrderStatus OrderStatus { get; init; }
    public override Guid StreamId => OrderId;
}