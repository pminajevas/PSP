using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid OrderId { get; set; }

        [Required]
        public Guid PaymentMethodId { get; set; }

        [Required]
        public PaymentStatusEnum Status { get; set; } = PaymentStatusEnum.Unpaid;

        [Required]
        public double Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        public Guid? ConfirmationId { get; set; }

        public Guid? CouponId { get; set; }
    }
}
