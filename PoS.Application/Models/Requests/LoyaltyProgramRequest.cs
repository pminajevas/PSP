using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class LoyaltyProgramRequest
    {
        [Required]
        public Guid? BusinessId { get; set; }
        public double? PointsPerPurchase { get; set; }
        [MaxLength(500)]
        public string RedemptionRules { get; set; }
        [MaxLength(500)]
        public string SpecialBenefits { get; set; }
    }
}
