using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class LoyaltyProgramRequest
    {
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
