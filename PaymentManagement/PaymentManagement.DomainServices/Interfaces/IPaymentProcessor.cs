using PaymentManagement.Domain.Entities;

namespace PaymentManagement.DomainServices.Interfaces;

public interface IPaymentProcessor
{
    Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId);
    Task CancelPaymentAsync(Guid paymentId);
}