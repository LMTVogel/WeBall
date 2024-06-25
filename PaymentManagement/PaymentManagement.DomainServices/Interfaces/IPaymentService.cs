using PaymentManagement.Domain.Entities;

namespace PaymentManagement.DomainServices.Interfaces;

public interface IPaymentService
{
    Task<Payment?> GetPaymentAsync(Guid paymentId);
    Task<Guid> CreatePaymentAsync(Order order);
    Task CancelPaymentAsync(Guid paymentId);
    Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId);
}