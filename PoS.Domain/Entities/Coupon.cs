using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{

    public class Coupon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }

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
