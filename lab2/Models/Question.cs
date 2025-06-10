using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{
    [Table("question")]
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Column("question")]
        [Required, StringLength(1000)]
        public string Text { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Answer { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [ForeignKey("user_id")]
        public User? User { get; set; }
    }
}
