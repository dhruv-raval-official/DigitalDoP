using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DigitalDoP.Models
{
    public class Staff
    {
        [Key]
        public int Id { get; set; }

        [Required, ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required, ForeignKey("Office")]
        public int OfficeId { get; set; }
        public Office Office { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
