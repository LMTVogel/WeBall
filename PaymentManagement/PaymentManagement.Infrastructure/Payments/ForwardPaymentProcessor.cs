using PaymentManagement.Domain.Entities;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.Infrastructure.Payments;

public class ForwardPaymentProcessor: IPaymentProcessor
{
    public Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }

    public Task CancelPaymentAsync(Guid paymentId)
    {
        throw new NotImplementedException();
    }
}