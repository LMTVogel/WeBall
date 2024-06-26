namespace CustomerAccountManagement.DomainServices.Interfaces;

public interface ICustomerIntegration
{
    Task ImportExternalCustomers();
}