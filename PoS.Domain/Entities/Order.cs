using PoS.Core.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PoS.Core.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid? Id { get; set; }

        [Required]
        public Guid? CustomerId { get; set; }

        [Required]
        public Guid? BusinessId { get; set; }

        public Guid? StaffId { get; set; }

        public Guid? TaxId { get; set; }

        public double Tips { get; set; } = 0;

        [Required]
        public OrderStatusEnum? Status { get; set; }

        [Required]
        public DateTime? Date { get; set; }

        [Required]
        public double? TotalAmount { get; set; }

        public double? Tip { get; set; }
    }
}
