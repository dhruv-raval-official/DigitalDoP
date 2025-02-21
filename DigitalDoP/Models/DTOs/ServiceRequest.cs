using System.ComponentModel.DataAnnotations;

namespace DigitalDoP.Models.DTOs
{
    public class ServiceRequest
    {
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

        [Required] // ✅ Ensures OfficeId is always provided
        public int OfficeId { get; set; }
    }

}
