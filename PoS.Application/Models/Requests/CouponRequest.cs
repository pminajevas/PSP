using System.ComponentModel.DataAnnotations;

namespace PoS.Application.Models.Requests
{
    public class CouponRequest
    {
        [Required]
        public Guid? BusinessId { get; set; }
        [Required]
        public double? Amount { get; set; }
        public enum ValidityEnum
        {
            TrueEnum = 0,
            FalseEnum = 1
        }
        public ValidityEnum? Validity { get; set; }
        public DateTime? ValidUntil { get; set; }
    }
}
