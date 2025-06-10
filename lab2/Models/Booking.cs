using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    [Table("booking")]
    public class Booking
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [ForeignKey("user_id")]
        public User? User { get; set; }

        [ForeignKey("trip_id")]
        public Trip? Trip { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
