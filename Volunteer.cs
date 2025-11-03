using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class Volunteer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public string Skills { get; set; }
        public string Availability { get; set; } // Weekdays, Weekends, Anytime
        public string PreferredLocation { get; set; }
        public bool HasTransportation { get; set; }
        public string EmergencyContact { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
    }
}