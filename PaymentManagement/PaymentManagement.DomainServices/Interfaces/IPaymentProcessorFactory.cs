using PaymentManagement.Domain.Entities;

namespace PaymentManagement.DomainServices.Interfaces;

public interface IPaymentProcessorFactory
{
    IPaymentProcessor? GetPaymentProcessor(PaymentMethod paymentMethod);
}