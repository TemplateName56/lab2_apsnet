using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required, StringLength(255)]
        public string Username { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool Admin { get; set; } = false;

        public ICollection<Question>? Questions { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
