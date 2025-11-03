using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class VolunteerTask
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [Required]
        public string RequiredSkills { get; set; }

        public int MaxVolunteers { get; set; }
        public int CurrentVolunteers { get; set; } = 0;

        public int? DisasterIncidentId { get; set; }
        public DisasterIncident DisasterIncident { get; set; }

        public string Status { get; set; } = "Open"; // Open, In Progress, Completed, Cancelled

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}