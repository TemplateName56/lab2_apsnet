using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab2.Models
{

    [Table("proposed_destination")]
    public class ProposedDestination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public int VoteCount { get; set; } = 0;

        public void IncrementVote() => VoteCount++;
    }
}
