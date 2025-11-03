using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}