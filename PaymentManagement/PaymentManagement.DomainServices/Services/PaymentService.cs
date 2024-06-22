using MassTransit;
using PaymentManagement.Domain.Entities;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Services;

public class PaymentService(
    IEventStore<PaymentEvent> eventStore,
    IPaymentProcessorFactory processorFactory,
    IPublishEndpoint serviceBus)
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

    public async Task<Guid> CreatePaymentAsync(Order order)
    {
        var payment = new Payment
        {
            Id = new Guid(),
            Amount = order.Products.Sum(p => p.Price),
            Status = PaymentStatus.Pending,
            PaymentMethod = order.PaymentMethod,
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
        await eventStore.AppendAsync(@event);
        await serviceBus.Publish(@event);

        return payment.Id;
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
        await serviceBus.Publish(@event);
    }

    public async Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId)
    {
        var payment = await GetPaymentAsync(paymentId);
        if (payment == null) throw new Exception("Payment not found");

        var processor = processorFactory.GetPaymentProcessor(payment.PaymentMethod);
        if (processor == null) throw new Exception("Payment processor not found");

        var result = await processor.ProcessPaymentAsync(payment.Id);
        if (result is PaymentStatus.Failed or PaymentStatus.Cancelled)
        {
            await CancelPaymentAsync(paymentId);
            return result;
        }

        var @event = new PaymentPaid
        {
            PaymentId = payment.Id,
            OrderId = payment.Order.Id,
            Status = PaymentStatus.Paid,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);
        await serviceBus.Publish(@event);

        return result;
    }
}