using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{
    public class Discount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public string DiscountName { get; set; }

        [Required]
        [Range(0, 1)]
        public double? DiscountPercentage { get; set; }

        public DateTime? ValidUntil { get; set; }

    }
}
