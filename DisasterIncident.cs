using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class DisasterIncident
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string DisasterType { get; set; } // Flood, Earthquake, Fire, etc.

        public DateTime ReportedDate { get; set; } = DateTime.UtcNow;
        public DateTime IncidentDate { get; set; }

        [Required]
        public string Severity { get; set; } // Low, Medium, High, Critical

        public int ReportedByUserId { get; set; }
        public User ReportedByUser { get; set; }

        public bool IsVerified { get; set; } = false;
        public bool IsActive { get; set; } = true;
    }
}