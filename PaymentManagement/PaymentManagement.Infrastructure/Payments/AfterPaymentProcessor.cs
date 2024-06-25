using PaymentManagement.Domain.Entities;
using PaymentManagement.DomainServices.Interfaces;

namespace PaymentManagement.Infrastructure.Payments;

public class AfterPaymentProcessor : IPaymentProcessor
{
    public async Task<PaymentStatus> ProcessPaymentAsync(Guid paymentId)
    {
        var msMockDelay = new Random().Next(200, 400);
        await Task.Delay(msMockDelay);

        var status = new Random().Next(0, 100);
        return status switch
        {
            < 90 => PaymentStatus.Paid,
            _ => PaymentStatus.Failed,
        };
    }

    public async Task CancelPaymentAsync(Guid paymentId)
    {
        var msMockDelay = new Random().Next(200, 400);
        await Task.Delay(msMockDelay);
    }
}