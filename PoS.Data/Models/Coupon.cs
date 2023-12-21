using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{

    public class Coupon
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

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
