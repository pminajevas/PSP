using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public  class LoyaltyProgram
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }


        public double? PointsPerPurchase { get; set; }

        [MaxLength(500)]
        public string RedemptionRules { get; set; }

        [MaxLength(500)]
        public string SpecialBenefits { get; set; }
    }
}
