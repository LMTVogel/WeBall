using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSupportManagement.Domain.Entities;

public class SupportAgent
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Email { get; set; }
}