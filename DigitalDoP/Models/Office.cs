using System.ComponentModel.DataAnnotations;

namespace DigitalDoP.Models
{
    public class Office
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string OfficeName { get; set; }

        public string Location { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
