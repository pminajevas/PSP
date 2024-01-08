using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class CouponRequest
    {
        [Required]
        public Guid BusinessId { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public CouponValidityEnum Validity { get; set; }

        [Required]
        public DateTime ValidUntil { get; set; }
    }
}
