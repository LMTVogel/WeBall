using PaymentManagement.Domain.Entities;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.Infrastructure.Payments;

public class PaymentProcessorFactory(IServiceProvider provider) : IPaymentProcessorFactory
{
    public IPaymentProcessor? GetPaymentProcessor(PaymentMethod paymentMethod)
    {
        return paymentMethod switch
        {
            PaymentMethod.Forward => provider.GetService(typeof(ForwardPaymentProcessor)) as IPaymentProcessor,
            PaymentMethod.AfterPay => provider.GetService(typeof(AfterPaymentProcessor)) as IPaymentProcessor,
            _ => throw new ArgumentOutOfRangeException(nameof(paymentMethod), paymentMethod, null)
        };
    }
}