using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{
    public class Tax
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string TaxName { get; set; }

        [MaxLength(500)]
        public string TaxDescription { get; set; }

        [Required]
        [Range(0, 1)]
        public double? TaxPercentage { get; set; }

        [Required]
        public CategoryEnum? Category { get; set; }

        [Required]
        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidUntil { get; set; }

        public enum CategoryEnum
        {
            FlatEnum = 0,
            PercentEnum = 1
        }
    }
}
