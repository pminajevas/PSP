using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class TaxRequest
    {
        [Required]
        [MaxLength(50)]
        public string TaxName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? TaxDescription { get; set; }

        [Required]
        public double TaxValue { get; set; }

        [Required]
        public TaxCategoryEnum Category { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }
    }
}
