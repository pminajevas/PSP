using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Discount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string DiscountName { get; set; } = string.Empty;

        [Required]
        [Range(0, 1)]
        public double DiscountPercentage { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }

    }
}
