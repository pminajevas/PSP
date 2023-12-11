using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Payment
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? OrderId { get; set; }

        [Required]
        public Guid? PaymentMethodId { get; set; }

        public enum StatusEnum
        {
            PaidEnum = 0,
            UnpaidEnum = 1,
            ProcessingEnum = 2
        }

        [Required]
        public StatusEnum? Status { get; set; }

        [Required]
        public double? Amount { get; set; }

        [Required]
        public DateTime? PaymentDate { get; set; }
    }
}
