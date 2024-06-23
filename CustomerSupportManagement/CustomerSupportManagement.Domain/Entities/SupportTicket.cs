using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerSupportManagement.Domain.Entities;

public class SupportTicket
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Required]
    public string subject { get; set; }
    [Required]
    public string description { get; set; }
    [Required]
    public string customerName { get; set; }
    [Required]
    public string customerEmail { get; set; }
    public Guid supportAgentId { get; set; }
    public SupportAgent? supportAgent { get; set; }
    [Required]
    public bool status { get; set; }
}