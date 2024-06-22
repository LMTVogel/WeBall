using PaymentManagement.Domain.Entities;

namespace PaymentManagement.DomainServices.Interfaces;

public interface IPaymentService
{
    Task<Payment?> GetPaymentAsync(Guid paymentId);
    Task<Guid> CreatePaymentAsync(decimal amount);
    Task CancelPaymentAsync(Guid paymentId);
    Task UpdatePaymentAsync(Guid paymentId, Payment payment);
    Task<string> ProcessPaymentAsync(Guid paymentId);
}