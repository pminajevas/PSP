using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public  class LoyaltyProgram
    {
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
