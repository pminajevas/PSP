using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoS.Shared.RequestDTOs
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
