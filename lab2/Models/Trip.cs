using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    [Table("trip")]
    public class Trip
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? Price { get; set; }

        [ForeignKey("destination_id")]
        public Destination? Destination { get; set; }

        public ICollection<Booking>? Bookings { get; set; }
    }
}
