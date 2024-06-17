namespace SupplierManagement.Domain.Entities;

public class Supplier
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string RepName { get; set; }
    public string RepEmail { get; set; }
    public string Role { get; set; }
    public string Kvk { get; set; }
    public string city { get; set; }
    public string state { get; set; }
    public string country { get; set; }
    public string postalCode { get; set; }
    public string street { get; set; }
    public string houseNumber { get; set; }
    public bool status { get; set; }
    public DateTime createdDate { get; set; }
}