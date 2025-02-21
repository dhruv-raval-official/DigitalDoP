using System.ComponentModel.DataAnnotations;

namespace DigitalDoP.Models.DTOs
{
    public class FeedbackRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required] // ✅ Ensures OfficeId is always provided
        public int OfficeId { get; set; }

        [Required]
        public int Rating { get; set; }

        [Required]
        public string Comment { get; set; }

    }
}
