using Events;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PaymentManagement.Domain.Events;

namespace PaymentManagement.Domain.Entities;

public enum PaymentStatus
{
    Pending,
    Paid,
    Cancelled,
    Failed,
}

public enum PaymentMethod
{
    Forward,
    AfterPay,
}

public class Payment
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public PaymentStatus Status { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Order Order { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Apply(PaymentEvent @event)
    {
        switch (@event)
        {
            case PaymentCreated created:
                Apply(created);
                break;
            case PaymentCancelled cancelled:
                Apply(cancelled);
                break;
            case PaymentPaid paid:
                Apply(paid);
                break;
        }
    }

    private void Apply(PaymentCreated created)
    {
        Id = created.PaymentId;
        Amount = created.Amount;
        Status = created.Status;
        Order = created.Order;
        CreatedAt = created.CreatedAtUtc;
    }

    private void Apply(PaymentCancelled cancelled)
    {
        Status = cancelled.Status;
    }

    private void Apply(PaymentPaid paid)
    {
        Status = paid.Status;
    }
}