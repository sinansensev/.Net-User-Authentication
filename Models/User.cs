using System.ComponentModel.DataAnnotations;

namespace PayosferIdentity.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Password { get; set; }

        [MaxLength(255)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Role { get; set; }

        [MaxLength(255)]
        public string Surname { get; set; }
        public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpiration { get; set; }

        [MaxLength(255)]
        public string PhoneNumber { get; set; }
    }
}
