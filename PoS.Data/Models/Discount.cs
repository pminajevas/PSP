using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Discount
    {
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
