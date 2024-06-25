namespace CustomerSupportManagement.Domain.Events;

public class CustomerUpdated
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}