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

        private string GetStreet(string address)
        {
            var parts = address.Split(',');
            return parts[0].Trim();
        }

        private string GetCity(string address)
        {
            var parts = address.Split(',');
            return parts[1].Trim().Split(' ')[1].Trim();
        }

        private string GetZipCode(string address)
        {
            var parts = address.Split(',');
            return parts[1].Trim().Split(' ')[0].Trim();
        }
    }
}