using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class DiscountRequest
    {
        [Required]
        public string DiscountName { get; set; } = String.Empty;

        [Required]
        [Range(0, 1)]
        public double DiscountPercentage { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }
    }
}
