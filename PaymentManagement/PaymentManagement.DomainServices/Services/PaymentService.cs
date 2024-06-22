using PaymentManagement.Domain.Entities;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Services;

public class PaymentService(IEventStore<PaymentEvent> eventStore, IPaymentProcessorFactory processorFactory)
    : IPaymentService
{
    public async Task<Payment?> GetPaymentAsync(Guid paymentId)
    {
        var events = await eventStore.ReadAsync<PaymentCreated>(paymentId);
        if (events.Count == 0) return null;

        var payment = new Payment();
        foreach (var @event in events)
        {
            payment.Apply(@event);
        }

        return payment;
    }

    public Task<Guid> CreatePaymentAsync(Order order)
    {
        var payment = new Payment
        {
            Id = new Guid(),
            Amount = order.Products.Sum(p => p.Price),
            Status = PaymentStatus.Pending,
            PaymentMethod = Order.PaymentMethod,
            Order = order,
            CreatedAt = DateTime.UtcNow
        };

        var @event = new PaymentCreated
        {
            PaymentId = payment.Id,
            Amount = payment.Amount,
            Status = payment.Status,
            Order = payment.Order,
            CreatedAtUtc = payment.CreatedAt
        };
        eventStore.AppendAsync(@event);

        return Task.FromResult(@event.PaymentId);
    }

    public async Task CancelPaymentAsync(Guid paymentId)
    {
        var payment = await GetPaymentAsync(paymentId);
        if (payment == null) throw new Exception("Payment not found");

        var @event = new PaymentCancelled
        {
            PaymentId = payment.Id,
            Status = PaymentStatus.Cancelled,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);
    }

    public Task UpdatePaymentAsync(Guid paymentId, Payment payment)
    {
        throw new NotImplementedException();
    }

    public async Task<string> ProcessPaymentAsync(Guid paymentId)
    {
        var payment = await GetPaymentAsync(paymentId);
        if (payment == null) throw new Exception("Payment not found");

        var processor = processorFactory.GetPaymentProcessor(payment.PaymentMethod);
        if (processor == null) throw new Exception("Payment processor not found");

        var result = await processor.ProcessPaymentAsync(payment.Id);
        if (result != PaymentStatus.Paid) throw new Exception("Payment processing failed");

        var @event = new PaymentPaid
        {
            PaymentId = payment.Id,
            OrderId = payment.Order.Id,
            Status = PaymentStatus.Paid,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);

        return "Payment processed";
    }
}