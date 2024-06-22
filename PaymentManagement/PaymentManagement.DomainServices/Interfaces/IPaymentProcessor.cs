namespace PaymentManagement.DomainServices.Interfaces;

public interface IPaymentProcessor
{
    Task<string> ProcessPaymentAsync(Guid paymentId);
    Task CancelPaymentAsync(Guid paymentId);
}