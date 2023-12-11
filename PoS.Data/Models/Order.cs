using System.ComponentModel.DataAnnotations;

namespace PoS.Data
{
    public class Order
    {
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? CustomerId { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }

        public Guid? StaffId { get; set; }

        public Guid? TaxId { get; set; }

        public enum StatusEnum
        {
            PaidEnum = 0,
            UnpaidEnum = 1
        }

        [Required]
        public StatusEnum? Status { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public double? TotalAmount { get; set; }

        public double? Tip { get; set; }
    }
}
