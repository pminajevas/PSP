using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Data
{
    public class Payment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? OrderId { get; set; }

        [Required]
        public Guid? PaymentMethodId { get; set; }

        public enum PaymentStatusEnum
        {
            PaidEnum = 0,
            UnpaidEnum = 1,
            ProcessingEnum = 2
        }

        [Required]
        public PaymentStatusEnum? Status { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public DateTime? PaymentDate { get; set; }
    }
}
