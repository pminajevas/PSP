using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class LoyaltyProgram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public double PointsPerPurchase { get; set; }

        [Required]
        [MaxLength(500)]
        public string RedemptionRules { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string SpecialBenefits { get; set; } = string.Empty;
    }
}
