using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DigitalDoP.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TrackingId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required, EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        public string ServiceType { get; set; }

        public DateTime ExpectedDeliveryDate { get; set; }

        public string Status { get; set; }

        public DateTime? ArrivalDate { get; set; }
        public DateTime? DispatchDate { get; set; }
        public DateTime? DeliveredDate { get; set; }

        // ✅ Correct Foreign Key Mapping
        [Required, ForeignKey("Office")]
        public int OfficeId { get; set; }
        public Office Office { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
