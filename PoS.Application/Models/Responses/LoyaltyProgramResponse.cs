using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Responses
{
    public class LoyaltyProgramResponse
    {
        public Guid Id { get; set; }
        public Guid BusinessId { get; set; }
        public double PointsPerPurchase { get; set; }
        public string RedemptionRules { get; set; } = string.Empty;
        public string SpecialBenefits { get; set; } = string.Empty;
    }
}
