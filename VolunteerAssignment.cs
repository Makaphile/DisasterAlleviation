using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class VolunteerAssignment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int VolunteerId { get; set; }
        public Volunteer Volunteer { get; set; }

        [Required]
        public int TaskId { get; set; }
        public VolunteerTask Task { get; set; }

        public DateTime AssignmentDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Assigned"; // Assigned, Completed, Cancelled

        public string Notes { get; set; }
    }
}