using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerAccountManagement.Domain.Entities
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] public string Name { get; set; }

        [Required, EmailAddress] public string Email { get; set; }

        [Required] public string Street { get; set; }

        [Required] public string City { get; set; }

        [Required] public string ZipCode { get; set; }
    }
}