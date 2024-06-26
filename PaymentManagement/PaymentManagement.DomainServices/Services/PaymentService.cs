using Events;
using MassTransit;
using PaymentManagement.Domain.Entities;
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
        Console.WriteLine($"Getting payment with id {paymentId}");
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
        Console.WriteLine("Creating payment...");
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
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

    public async Task FailPaymentAsync(Guid paymentId)
    {
        Console.WriteLine($"Cancelling payment with id {paymentId}");
        var payment = await GetPaymentAsync(paymentId);
        if (payment == null) throw new Exception("Payment not found");

        var @event = new PaymentFailed
        {
            PaymentId = payment.Id,
            Status = PaymentStatus.Failed,
            OrderId = payment.Order.Id,
            CustomerEmail = payment.Order.CustomerEmail,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);
        await serviceBus.Publish(@event);
    }

    public async Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId)
    {
        Console.WriteLine($"Processing payment with id {paymentId}");
        var payment = await GetPaymentAsync(paymentId);
        if (payment == null)
        {
            Console.WriteLine($"Payment with id {paymentId} not found");
            throw new Exception("Payment not found");
        }

        var processor = processorFactory.GetPaymentProcessor(payment.PaymentMethod);
        if (processor == null)
        {
            Console.WriteLine($"Payment processor not found");
            throw new Exception("Payment processor not found");
        }

        var result = await processor.ProcessPaymentAsync(payment.Id);
        if (result is PaymentStatus.Failed)
        {
            await FailPaymentAsync(paymentId);
            return result;
        }

        var @event = new PaymentPaid
        {
            PaymentId = payment.Id,
            OrderId = payment.Order.Id,
            Status = PaymentStatus.Paid,
            CustomerEmail = payment.Order.CustomerEmail,
            CreatedAtUtc = DateTime.UtcNow
        };
        await eventStore.AppendAsync(@event);
        await serviceBus.Publish(@event);

        return result;
    }
}