using PaymentManagement.Domain.Entities;
using PaymentManagement.Domain.Events;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.DomainServices.Services;

public class PaymentService(IEventStore<PaymentEvent> eventStore, IPaymentProcessor paymentProcessor) : IPaymentService
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

    public Task<Guid> CreatePaymentAsync(decimal amount)
    {
        throw new NotImplementedException();
    }

    public Task CancelPaymentAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public Task UpdatePaymentAsync(Guid paymentId, Payment payment)
    {
        throw new NotImplementedException();
    }

    public Task<string> ProcessPaymentAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }
}