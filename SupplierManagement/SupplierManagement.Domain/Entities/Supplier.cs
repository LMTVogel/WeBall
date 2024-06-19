using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplierManagement.Domain.Entities;

public class Supplier
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string RepName { get; set; }
    [Required]
    public string RepEmail { get; set; }
    [Required]
    public string Role { get; set; }
    [MinLength(8), MaxLength(8)]
    public int Kvk { get; set; }
    [Required]
    public string city { get; set; }
    [Required, EmailAddress]
    public string state { get; set; }
    [Required]
    public string country { get; set; }
    [Required, MaxLength(20)]
    public string postalCode { get; set; }
    [Required]
    public string street { get; set; }
    [Required, MaxLength(10)]
    public string houseNumber { get; set; }
    [Required]
    public bool status { get; set; }
    [Required, DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
    public DateTime createdDate { get; set; }
}