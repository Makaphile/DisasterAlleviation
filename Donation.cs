using System.ComponentModel.DataAnnotations;

namespace DisasterAlleviationApp.Models
{
    public class Donation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string DonorName { get; set; }

        [Required]
        public string DonorEmail { get; set; }

        [Required]
        public string DonationType { get; set; } // Food, Clothing, Medical, Money

        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal? MonetaryValue { get; set; }

        public int? DisasterIncidentId { get; set; }
        public DisasterIncident DisasterIncident { get; set; }

        public DateTime DonationDate { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Pending"; // Pending, Received, Distributed

        public int? DonatedByUserId { get; set; }
        public User DonatedByUser { get; set; }
    }
}