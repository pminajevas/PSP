using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Tax
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TaxName { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? TaxDescription { get; set; }

        [Required]
        [Range(0, 1)]
        public double? TaxPercentage { get; set; }

        [Required]
        public TaxCategoryEnum Category { get; set; }

        [Required]
        public DateTime ValidFrom { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }
    }
}
